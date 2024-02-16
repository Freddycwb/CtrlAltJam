using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeAfterTimer : InvokeAfter
{
    [SerializeField] private float timeToAction;
    [SerializeField] private bool desableAfterTimer = true;
    private float timePassed;

    private Coroutine coroutine;

    public float GetTimePassed()
    {
        return timeToAction - timePassed;
    }

    private void OnEnable()
    {
        if (timeToAction > 0)
        {
            coroutine = StartCoroutine(InvokeAfterSeconds());
        }
    }

    private IEnumerator InvokeAfterSeconds()
    {
        for (float i = 0; i < timeToAction; i += Time.deltaTime)
        {
            timePassed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
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
