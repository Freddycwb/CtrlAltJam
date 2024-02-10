using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMove : MonoBehaviour
{
    [SerializeField] private GameObject input;
    private IInput _input;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxAccel;
    private bool _moving;

    public Action startMove;
    public Action endMove;

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

    public void SetMaxSpeed(float value)
    {
        maxSpeed = value;
    }

    private void FixedUpdate()
    {
        Vector3 dir = _input != null ? new Vector3(_input.direction.normalized.x, 0, _input.direction.normalized.y) : Vector3.zero;
        Move(dir);
        if (dir.magnitude > 0 && !_moving)
        {
            if (startMove != null)
            {
                startMove.Invoke();
            }
            _moving = true;
        }
        else if (dir.magnitude <= 0 && _moving)
        {
            if (endMove != null)
            {
                endMove.Invoke();
            }
            _moving = false;
        }
    }

    public void Move(Vector3 dir)
    {
        Vector3 goalVel = dir * maxSpeed;
        Vector3 neededAccel = goalVel - rb.velocity;
        neededAccel -= Vector3.up * neededAccel.y;
        neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);
        rb.AddForce(neededAccel, ForceMode.Impulse);
    }
}
