using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    [SerializeField] private Renderer visual;
    private float currentPaint;
    [SerializeField] private float paintToEarnPoint = 0.1f;
    private float currentPaintPoint;

    public Action paint;
    public Action fullPaint;

    public void Paint(Color color, float sprayIntensity)
    {
        if (currentPaint >= 1)
        {
            return;
        }
        visual.material.color = Color.Lerp(visual.material.color, color, Time.deltaTime * sprayIntensity);
        currentPaint += Time.deltaTime * sprayIntensity;
        currentPaintPoint += Time.deltaTime * sprayIntensity;
        if (currentPaintPoint >= paintToEarnPoint)
        {
            currentPaintPoint = 0;
            if (paint != null)
            {
                paint.Invoke();
            }
            Debug.Log("pinto");
        }
        if (currentPaint >= 1)
        {
            if (fullPaint != null)
            {
                fullPaint.Invoke();
            }
            Debug.Log("pinto tudo");
        }
    }
}
