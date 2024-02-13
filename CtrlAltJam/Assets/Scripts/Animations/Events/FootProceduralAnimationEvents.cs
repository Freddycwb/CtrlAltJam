using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FootProceduralAnimationEvents : MonoBehaviour
{
    [SerializeField] private FootProceduralAnimation footProceduralAnimation;

    [SerializeField] private UnityEvent onStartStep;
    [SerializeField] private UnityEvent onEndStep;

    [SerializeField] private UnityEvent onStartRightStep;
    [SerializeField] private UnityEvent onStartLeftStep;

    [SerializeField] private UnityEvent onEndRightStep;
    [SerializeField] private UnityEvent onEndLeftStep;

    void Start()
    {
        footProceduralAnimation.startStep += InvokeOnStartStep;
        footProceduralAnimation.endStep += InvokeOnEndStep;
        footProceduralAnimation.startRightStep += InvokeOnStartRightStep;
        footProceduralAnimation.startLeftStep += InvokeOnStartLeftStep;
        footProceduralAnimation.endRightStep += InvokeOnEndRightStep;
        footProceduralAnimation.endRightStep += InvokeOnEndLeftStep;
    }

    private void InvokeOnStartStep()
    {
        if (onStartStep != null)
        {
            onStartStep.Invoke();
        }
    }
    private void InvokeOnEndStep()
    {
        if (onEndStep != null)
        {
            onEndStep.Invoke();
        }
    }

    private void InvokeOnStartRightStep()
    {
        if (onStartRightStep != null)
        {
            onStartRightStep.Invoke();
        }
    }
    private void InvokeOnStartLeftStep()
    {
        if (onStartLeftStep != null)
        {
            onStartLeftStep.Invoke();
        }
    }

    private void InvokeOnEndRightStep()
    {
        if (onEndRightStep != null)
        {
            onEndRightStep.Invoke();
        }
    }
    private void InvokeOnEndLeftStep()
    {
        if (onEndLeftStep != null)
        {
            onEndLeftStep.Invoke();
        }
    }


    private void OnDestroy()
    {
        footProceduralAnimation.startStep -= InvokeOnStartStep;
        footProceduralAnimation.endStep -= InvokeOnEndStep;
        footProceduralAnimation.startRightStep -= InvokeOnStartRightStep;
        footProceduralAnimation.startLeftStep -= InvokeOnStartLeftStep;
        footProceduralAnimation.endRightStep -= InvokeOnEndRightStep;
        footProceduralAnimation.endRightStep -= InvokeOnEndLeftStep;
    }
}
