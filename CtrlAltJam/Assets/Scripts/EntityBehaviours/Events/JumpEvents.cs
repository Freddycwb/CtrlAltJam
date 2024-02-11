using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpEvents : MonoBehaviour
{
    [SerializeField] private Jump jump;

    [SerializeField] private UnityEvent onJump;

    void Start()
    {
        jump.jump += InvokeOnJump;
    }

    private void InvokeOnJump()
    {
        if (onJump != null)
        {
            onJump.Invoke();
        }
    }

    private void OnDestroy()
    {
        jump.jump -= InvokeOnJump;
    }
}
