using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyInput : MonoBehaviour, IInput
{
    [SerializeField] private GameObject dir;
    private EnemyDirection _dir;

    private bool _canControl = true;

    private void Start()
    {
        _dir = dir != null ? dir.GetComponent<EnemyDirection>() : null;
    }

    public Vector2 direction
    {
        get
        {
            if (_canControl && _dir != null)
            {
                return _dir.Direction();
            }
            else
            {
                return Vector2.zero;
            }
        }
    }

    public Vector2 look
    {
        get
        {
            return Vector2.zero;
        }
    }

    public Vector2 sensitivity
    {
        get
        {
            return Vector2.zero;
        }
    }

    public bool aButtonDown
    {
        get
        {
            return false;
        }
    }

    public bool aButton
    {
        get
        {
            return false;
        }
    }

    public bool aButtonUp
    {
        get
        {
            return false;
        }
    }

    public bool batButtonDown
    {
        get
        {
            return false;
        }
    }

    public bool batButton
    {
        get
        {
            return false;
        }
    }

    public bool batButtonUp
    {
        get
        {
            return false;
        }
    }

    public bool sprayButtonDown
    {
        get
        {
            return false;
        }
    }

    public bool sprayButton
    {
        get
        {
            return false;
        }
    }

    public bool sprayButtonUp
    {
        get
        {
            return false;
        }
    }
}
