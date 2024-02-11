using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRb : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        
    }

    
    void Update()
    {
        RemoveRigidbody();
    }
    private void RemoveRigidbody()
    {
        if (rb != null && rb.velocity.magnitude <= 0)
        {
            Destroy(GetComponent<Rigidbody>());
        }
    }

    public void JustGetRigidbody()
    {
        StartCoroutine(SetHaveRb());
    }
    
    private IEnumerator SetHaveRb()
    {
        yield return new WaitForSeconds(1f);
        rb = GetComponent<Rigidbody>();
    }
   
}
