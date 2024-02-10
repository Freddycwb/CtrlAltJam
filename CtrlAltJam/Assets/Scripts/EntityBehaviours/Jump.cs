using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private GameObject input;
    private IInput _input;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Gravity gravity;

    [SerializeField] private float jumpForce;
    [SerializeField] private float holdJumpTime;
    private float _holdJump;
    [SerializeField] private float jumpPressedRememberTime;
    private float _jumpPressedRemember;
    [SerializeField] private float groundedRememberTime;
    private float _groundedRemember;
    private bool _justJumped;

    public Action jump;


    private void Start()
    {
        if (input == null && GetComponent<IInput>() != null)
        {
            _input = GetComponent<IInput>();
        }
        else if (input != null && input.GetComponent<IInput>() != null)
        {
            _input = input.GetComponent<IInput>();
        }

        if (rb == null && GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        SetRemembers();
    }

    private void FixedUpdate()
    {
        PerformJump();
    }

    private void SetRemembers()
    {
        if (_groundedRemember > 0)
        {
            _groundedRemember -= Time.deltaTime;
        }
        if (gravity.GetIsGrounded() && !_justJumped)
        {
            _groundedRemember = groundedRememberTime;
        }
        if (_jumpPressedRemember > 0)
        {
            _jumpPressedRemember -= Time.deltaTime;
        }
        if (_input.aButtonDown)
        {
            _jumpPressedRemember = jumpPressedRememberTime;
        }
    }

    private void PerformJump()
    {
        if ((_input.aButton && _justJumped) || ((_jumpPressedRemember > 0) && (_groundedRemember > 0)))
        {
            if (!_justJumped)
            {
                _justJumped = true;
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                _holdJump = holdJumpTime;
                _jumpPressedRemember = 0;
                _groundedRemember = 0;
                if (jump != null)
                {
                    jump.Invoke();
                }
            }
            else
            {
                if (_holdJump > 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                    _holdJump -= Time.deltaTime;
                }
            }
        }
        else
        {
            _justJumped = false;
        }
    }
}
