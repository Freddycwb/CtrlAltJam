using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityEvents : MonoBehaviour
{
    [SerializeField] private Gravity gravity;

    [SerializeField] private UnityEvent onLand;

    void Start()
    {
        gravity.land += InvokeOnLand;
    }

    private void InvokeOnLand()
    {
        if (onLand != null)
        {
            onLand.Invoke();
        }
    }

    private void OnDestroy()
    {
        gravity.land -= InvokeOnLand;
    }
}
