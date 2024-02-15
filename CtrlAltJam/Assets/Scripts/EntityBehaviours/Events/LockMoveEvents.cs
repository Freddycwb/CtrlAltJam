using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockMoveEvents : MonoBehaviour
{
    [SerializeField] private LockMove lockMove;

    [SerializeField] private UnityEvent onLock;
    [SerializeField] private UnityEvent onFree;

    void Start()
    {
        lockMove.onLock += InvokeOnLock;
        lockMove.onFree += InvokeOnFree;
    }

    private void InvokeOnLock()
    {
        if (onLock != null)
        {
            onLock.Invoke();
        }
    }

    private void InvokeOnFree()
    {
        if (onFree != null)
        {
            onFree.Invoke();
        }
    }

    private void OnDestroy()
    {
        lockMove.onLock -= InvokeOnLock;
        lockMove.onFree -= InvokeOnFree;
    }
}
