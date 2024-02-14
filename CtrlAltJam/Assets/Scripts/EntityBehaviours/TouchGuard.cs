using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGuard : MonoBehaviour
{
    [SerializeField] private LockMove lockMove;

    private void OnTriggerEnter(Collider other)
    {
        lockMove.Lock(other.GetComponent<Throwable>());
    }
}
