using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private float forceImpulse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Throwable>() != null)
        {
            other.GetComponent<Throwable>().JustGetHit(transform.forward * forceImpulse);
        }
    }
}

