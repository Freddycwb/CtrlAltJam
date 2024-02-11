using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Throw : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float forceImpulse;

    void Start()
    {
        rb = null;
    }

    
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObjsToThrow" && rb == null)
        {
            rb = other.AddComponent<Rigidbody>();
            other.GetComponent<RemoveRb>().JustGetRigidbody();

        }

        if (rb != null)
        {
            rb.AddForce(Vector3.up * forceImpulse, ForceMode.Impulse);
        }
    }
}

