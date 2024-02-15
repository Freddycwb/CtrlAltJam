using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PaintableleEvents : MonoBehaviour
{
    [SerializeField] private Paintable paintable;

    [SerializeField] private UnityEvent onPaint;
    [SerializeField] private UnityEvent onFullPaint;

    void Start()
    {
        paintable.paint += InvokeOnPaint;
        paintable.fullPaint += InvokeOnFullPaint;
    }

    private void InvokeOnPaint()
    {
        if (onPaint != null)
        {
            onPaint.Invoke();
        }
    }

    private void InvokeOnFullPaint()
    {
        if (onFullPaint != null)
        {
            onFullPaint.Invoke();
        }
    }

    private void OnDestroy()
    {
        paintable.paint -= InvokeOnPaint;
        paintable.fullPaint -= InvokeOnFullPaint;
    }
}
