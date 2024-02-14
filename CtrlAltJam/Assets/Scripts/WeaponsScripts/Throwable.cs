using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    private bool _haveRigidbody;
    private bool _delayTimePass = false;
    [SerializeField] private float mass;
    private bool _getHitBefore;

    public Action<GameObject> getHitForTheFirstTime;
    public Action<GameObject> getHit;

    void Start()
    {
        _haveRigidbody = rb != null;
    }

    
    void Update()
    {
        RemoveRigidbody();
    }
    private void RemoveRigidbody()
    {
        if (!_haveRigidbody && rb != null && rb.velocity.magnitude <= 0 && _delayTimePass)
        {
            Destroy(GetComponent<Rigidbody>());
            _delayTimePass = false;
        }
    }

    public void JustGetHit(Vector3 force)
    {
        if (!_haveRigidbody)
        {
            gameObject.AddComponent<Rigidbody>();
            GetComponent<Rigidbody>().mass = mass;
            rb = GetComponent<Rigidbody>();
            StartCoroutine(SetHaveRb());
        }
        if (_getHitBefore)
        {
            if (getHit != null)
            {
                getHit.Invoke(gameObject);
            }
        }
        else
        {
            if (getHitForTheFirstTime != null)
            {
                getHitForTheFirstTime.Invoke(gameObject);
            }
        }
        rb.AddForce(force, ForceMode.Impulse);
    }
    
    private IEnumerator SetHaveRb()
    {
        yield return new WaitForSeconds(1f);
        _delayTimePass = true;
    }
   
}
