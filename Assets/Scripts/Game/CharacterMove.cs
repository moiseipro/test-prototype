using System;
using System.Collections;
using System.Collections.Generic;
using SimpleInputNamespace;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    private CharacterController _characterController;
    private Animator _animator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(SimpleInput.GetAxis("Horizontal"), 0, SimpleInput.GetAxis("Vertical"));
        _animator.SetFloat("speed", _characterController.velocity.magnitude);
        _characterController.Move(moveDirection * speed * Time.fixedDeltaTime);
        if(moveDirection.magnitude>0.1f) transform.rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
    }
}
