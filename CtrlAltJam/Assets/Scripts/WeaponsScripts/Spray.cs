using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spray : MonoBehaviour
{
    [SerializeField] private Color colorSpray;
    [SerializeField] private float sprayIntensity = 0.1f;
    [SerializeField] private float sprayDistance = 5;
    [SerializeField] private GameObject damage;

    [SerializeField] private GameObject input;
    private IInput _input;

    public Action startPaint;
    public Action stopPaint;

    private void Start()
    {
        if (input == null && GetComponent<IInput>() != null)
        {
            _input = GetComponent<IInput>();
        }
        else if (input != null && input.GetComponent<IInput>() != null)
        {
            _input = input.GetComponent<IInput>();
        }

        damage.GetComponent<Painter>().sprayIntensity = sprayIntensity;
    }

    void Update()
    {
        if (_input.sprayButtonDown)
        {
            damage.SetActive(true);
            damage.GetComponent<Painter>().colorSpray = colorSpray;
            if (startPaint != null)
            {
                startPaint.Invoke();
            }
        }

        if (_input.sprayButtonUp)
        {
            damage.SetActive(false);
            colorSpray = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            if (stopPaint != null)
            {
                stopPaint.Invoke();
            }
        }
    }

}
