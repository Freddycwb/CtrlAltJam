using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SprayEvents : MonoBehaviour
{
    [SerializeField] private Spray spray;

    [SerializeField] private UnityEvent onStartPaint;
    [SerializeField] private UnityEvent onStopPaint;

    void Start()
    {
        spray.startPaint += InvokeOnStartPaint;
        spray.stopPaint += InvokeOnStopPaint;
    }

    private void InvokeOnStartPaint()
    {
        if (onStartPaint != null)
        {
            onStartPaint.Invoke();
        }
    }

    private void InvokeOnStopPaint()
    {
        if (onStopPaint != null)
        {
            onStopPaint.Invoke();
        }
    }

    private void OnDestroy()
    {
        spray.startPaint -= InvokeOnStartPaint;
        spray.stopPaint -= InvokeOnStopPaint;
    }
}
