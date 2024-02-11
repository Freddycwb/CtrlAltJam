using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private float forceImpulse;

    void Start()
    {
  
    }
    
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ObjsToThrow")
        {
            other.GetComponent<RemoveRb>().JustGetRigidbody();
        }

        if (other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Rigidbody>().AddForce(transform.forward * forceImpulse, ForceMode.Impulse);
        }
    }
}

