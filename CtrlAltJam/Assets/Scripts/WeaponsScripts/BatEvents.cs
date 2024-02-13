using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BatEvents : MonoBehaviour
{
    [SerializeField] private Bat bat;

    [SerializeField] private UnityEvent onAttack;

    void Start()
    {
        bat.attack += InvokeOnAttack;
    }

    private void InvokeOnAttack()
    {
        if (onAttack != null)
        {
            onAttack.Invoke();
        }
    }

    private void OnDestroy()
    {
        bat.attack -= InvokeOnAttack;
    }
}
