using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowableEvents : MonoBehaviour
{
    [SerializeField] private Throwable throwable;

    [SerializeField] private UnityEvent onHit;
    [SerializeField] private UnityEvent onHitForTheFirstTime;

    void Start()
    {
        throwable.getHit += InvokeOnHit;
        throwable.getHitForTheFirstTime += InvokeOnHitForTheFirstTime;
    }

    private void InvokeOnHit(GameObject value)
    {
        if (onHit != null)
        {
            onHit.Invoke();
        }
    }

    private void InvokeOnHitForTheFirstTime(GameObject value)
    {
        if (onHitForTheFirstTime != null)
        {
            onHitForTheFirstTime.Invoke();
        }
    }

    private void OnDestroy()
    {
        throwable.getHit -= InvokeOnHit;
        throwable.getHitForTheFirstTime -= InvokeOnHitForTheFirstTime;
    }
}
