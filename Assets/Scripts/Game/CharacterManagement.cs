using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using GardenManagement;
using SimpleInputNamespace;
using UnityEngine;

public enum HeroState{
    Free = 1,
    Mow = 2
}

[RequireComponent(typeof(CharacterController))]
public class CharacterManagement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float gravity = 9.8f;

    private HeroState _heroState = HeroState.Free;

    private CharacterController _characterController;
    private CharacterAnimation _characterAnimation;
    private GardenBed _gardenBed;
    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _characterAnimation = GetComponentInChildren<CharacterAnimation>();
        _characterAnimation.CutTheGrass += MowedDown;
    }

    private void FixedUpdate()
    {
        Move();
        //Gravity();
        Debug.Log(_characterController.velocity.magnitude);
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(SimpleInput.GetAxis("Horizontal") * speed, Gravity(), SimpleInput.GetAxis("Vertical") * speed);
        _characterController.Move(moveDirection  * Time.fixedDeltaTime);
        _characterAnimation.Move(_characterController.velocity.magnitude);
        Rotation(moveDirection);
    }

    private float Gravity()
    {
        if (!_characterController.isGrounded) return -gravity;
        else return 0;
    }

    private void Rotation(Vector3 direction)
    {
        direction.y = 0;
        if(direction.magnitude > 0.15f) transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_characterController.velocity.magnitude < 0.15f)
        {
            _gardenBed = other.GetComponent<GardenBed>();
            if (_gardenBed && !_gardenBed.IsCutted && _heroState == HeroState.Free)
            {
                _heroState = HeroState.Mow;
                _characterAnimation.Mow();
                Debug.Log("Triggered");
            }
        }
        else
        {
            _gardenBed = null;
            _heroState = HeroState.Free;
        }
    }

    private void MowedDown()
    {
        if (_gardenBed)
        {
            _gardenBed.Cut();
            _gardenBed = null;
            _heroState = HeroState.Free;
        }
    }
}
