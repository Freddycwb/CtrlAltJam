using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldOfViewEvents : MonoBehaviour
{
    [SerializeField] private FieldOfView fov;

    [SerializeField] private UnityEvent targetEnterFOV;
    [SerializeField] private UnityEvent targetExitFOV;

    private void Start()
    {
        fov.targetEnterFOV += InvokeTargetEnterFOVEvent;
        fov.targetExitFOV += InvokeTargetExitFOVEvent;
    }

    private void InvokeTargetEnterFOVEvent()
    {
        if (targetEnterFOV != null)
        {
            targetEnterFOV.Invoke();
        }
    }

    private void InvokeTargetExitFOVEvent()
    {
        if (targetExitFOV != null)
        {
            targetExitFOV.Invoke();
        }
    }

    private void OnDestroy()
    {
        fov.targetEnterFOV -= InvokeTargetEnterFOVEvent;
        fov.targetExitFOV -= InvokeTargetExitFOVEvent;
    }
}
