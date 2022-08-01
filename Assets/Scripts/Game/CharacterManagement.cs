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
        _characterAnimation.FinisMow += LoseTarget;
    }

    private void FixedUpdate()
    {
        Move();
        CheckingGrass();
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
        if(direction.magnitude > 0.2f) transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void CheckingGrass()
    {
        RaycastHit hit;
        Vector3 castPosition = transform.position+Vector3.up;
        LayerMask layerMask = LayerMask.GetMask("Grass");

        var collision = Physics.OverlapSphere(castPosition, _characterController.height / 2f, layerMask);
        Debug.Log(collision.Length);
        if (collision.Length>0)
        {
            Debug.Log(_heroState);
            _gardenBed = collision[0].GetComponent<GardenBed>();
            if (_characterController.velocity.magnitude < 0.2f)
            {
                if (_gardenBed && !_gardenBed.IsCutted && _heroState == HeroState.Free)
                {
                    _heroState = HeroState.Mow;
                    _characterAnimation.Mow();
                }
            }
            else
            {
                if(_heroState == HeroState.Mow) LoseTarget();
            }
        }
    }

    private void MowedDown()
    {
        if (_gardenBed)
        {
            _gardenBed.Cut();
        }
    }

    private void LoseTarget()
    {
        _heroState = HeroState.Free;
        _gardenBed = null;
        _characterAnimation.StopMow();
    }
}
