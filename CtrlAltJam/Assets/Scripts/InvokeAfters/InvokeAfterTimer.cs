using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeAfterTimer : InvokeAfter
{
    [SerializeField] private float timeToAction;
    [SerializeField] private bool desableAfterTimer = true;

    private Coroutine coroutine;

    private void OnEnable()
    {
        if (timeToAction > 0)
        {
            coroutine = StartCoroutine(InvokeAfterSeconds());
        }
    }

    private IEnumerator InvokeAfterSeconds()
    {
        yield return new WaitForSeconds(timeToAction);
        CallAction();
        enabled = !desableAfterTimer;
    }

    public void SetTimerToAction(float Time)
    {
        timeToAction = Time;
        coroutine = StartCoroutine(InvokeAfterSeconds());
    }

    private void OnDisable()
    {
        StopCoroutine(coroutine);
    }
}
