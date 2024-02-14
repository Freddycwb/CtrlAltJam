using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SuspicionManageEvents : MonoBehaviour
{
    [SerializeField] private SuspicionManager suspicion;

    [SerializeField] private UnityEvent startLooking;
    [SerializeField] private UnityEvent startChase;
    [SerializeField] private UnityEvent checkLastPosition;
    [SerializeField] private UnityEvent continueChase;
    [SerializeField] private UnityEvent stopLooking;
    [SerializeField] private UnityEvent loseSight;

    private void Start()
    {
        suspicion.startLooking += StartLooking;
        suspicion.startChase += StartChase;
        suspicion.checkLastPosition += CheckLastPosition;
        suspicion.continueChase += ContinueChase;
        suspicion.stopLooking += StopLooking;
        suspicion.loseSight += LoseSight;
    }

    private void StartLooking()
    {
        if (startLooking != null)
        {
            startLooking.Invoke();
        }
    }
    private void StartChase()
    {
        if (startChase != null)
        {
            startChase.Invoke();
        }
    }
    private void CheckLastPosition()
    {
        if (checkLastPosition != null)
        {
            checkLastPosition.Invoke();
        }
    }
    private void ContinueChase()
    {
        if (continueChase != null)
        {
            continueChase.Invoke();
        }
    }
    private void StopLooking()
    {
        if (stopLooking != null)
        {
            stopLooking.Invoke();
        }
    }
    private void LoseSight()
    {
        if (loseSight != null)
        {
            loseSight.Invoke();
        }
    }

    private void OnDestroy()
    {
        suspicion.startLooking -= StartLooking;
        suspicion.startChase -= StartChase;
        suspicion.checkLastPosition -= CheckLastPosition;
        suspicion.continueChase -= ContinueChase;
        suspicion.stopLooking -= StopLooking;
        suspicion.loseSight -= LoseSight;
    }
}
