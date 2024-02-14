using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : MonoBehaviour
{
    [SerializeField] private SuspicionManager suspicionManager;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask targetMask;

    public void NotifyPlayerPos()
    {
        Collider[] enemiesNear = Physics.OverlapSphere(transform.position, radius, targetMask);
        foreach (Collider e in enemiesNear)
        {
            SuspicionManager eSuspicionManager = e.GetComponent<SuspicionManager>();
            if (eSuspicionManager != null && eSuspicionManager != suspicionManager)
            {
                eSuspicionManager.Alert(suspicionManager.GetPlayerLastPosition());
            }
        }
    }
}
