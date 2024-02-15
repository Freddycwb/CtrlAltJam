using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private float forceImpulse;
    [SerializeField] private List<Collider> alreadyThrow = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Throwable>() != null && !alreadyThrow.Contains(other))
        {
            other.GetComponent<Throwable>().JustGetHit(transform.forward * forceImpulse);
            alreadyThrow.Add(other);
        }
    }
}

