using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spray : MonoBehaviour
{
    [SerializeField] private Color colorSpray;
    [SerializeField] private float sprayIntensity = 0.1f;
    [SerializeField] private float sprayDistance = 5;
    [SerializeField] private GameObject collider;

    [SerializeField] private GameObject input;
    private IInput _input;

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

        collider.GetComponent<Painter>().sprayIntensity = sprayIntensity;
    }

    void Update()
    {
        if (_input.sprayButtonDown)
        {
            collider.SetActive(true);
            collider.GetComponent<Painter>().colorSpray = colorSpray;
        }

        if (_input.sprayButtonUp)
        {
            collider.SetActive(false);
            colorSpray = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

}
