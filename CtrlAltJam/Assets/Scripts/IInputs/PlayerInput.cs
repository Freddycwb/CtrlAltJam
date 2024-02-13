using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, IInput
{
    [SerializeField] private Vector2Variable playerSensitivity;
    [SerializeField] private GameObjectVariable cameraObject;

    private bool _canControl = true;

    public Vector2 direction
    {
        get
        {
            if (!_canControl)
            {
                return Vector2.zero;
            }

            Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 rotatedMove = Vector2.zero;

            if (cameraObject != null && cameraObject.value != null)
            {
                float headAngle = Mathf.Deg2Rad * (360 - cameraObject.value.transform.rotation.eulerAngles.y);

                Vector2 a = new Vector2(Mathf.Cos(headAngle), Mathf.Sin(headAngle));
                Vector2 b = new Vector2(-Mathf.Sin(headAngle), Mathf.Cos(headAngle));

                rotatedMove = move.x * a + move.y * b;
            }

            return cameraObject != null && cameraObject.value != null ? rotatedMove : move;
        }
    }

    public Vector2 look
    {
        get
        {
            if (!_canControl)
            {
                return Vector2.zero;
            }

            Vector2 mouseLook = new Vector2(Mouse.current.delta.value.y, Mouse.current.delta.value.x);

            return mouseLook;
        }
    }

    public Vector2 sensitivity
    {
        get
        {
            return playerSensitivity.value != null ? playerSensitivity.value : Vector2.zero;
        }
    }

    public bool aButtonDown
    {
        get
        {
            if (!_canControl)
            {
                return false;
            }
            return Input.GetKeyDown(KeyCode.Space);
        }
    }

    public bool aButton
    {
        get
        {
            if (!_canControl)
            {
                return false;
            }
            return Input.GetKey(KeyCode.Space);
        }
    }

    public bool aButtonUp
    {
        get
        {
            if (!_canControl)
            {
                return false;
            }
            return Input.GetKeyUp(KeyCode.Space);
        }
    }

    public bool batButtonDown
    {
        get
        {
            if (!_canControl)
            {
                return false;
            }
            return Input.GetMouseButtonDown(1);
        }
    }

    public bool batButton
    {
        get
        {
            if (!_canControl)
            {
                return false;
            }
            return Input.GetMouseButton(1);
        }
    }

    public bool batButtonUp
    {
        get
        {
            if (!_canControl)
            {
                return false;
            }
            return Input.GetMouseButtonUp(1);
        }
    }

    public bool sprayButtonDown
    {
        get
        {
            if (!_canControl)
            {
                return false;
            }
            return Input.GetMouseButtonDown(0);
        }
    }

    public bool sprayButton
    {
        get
        {
            if (!_canControl)
            {
                return false;
            }
            return Input.GetMouseButton(0);
        }
    }

    public bool sprayButtonUp
    {
        get
        {
            if (!_canControl)
            {
                return false;
            }
            return Input.GetMouseButtonUp(0);
        }
    }
}
