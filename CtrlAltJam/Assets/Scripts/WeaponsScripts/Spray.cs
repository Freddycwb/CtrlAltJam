using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spray : MonoBehaviour
{
    [SerializeField] private Color colorSpray;
    [SerializeField] private float sprayIntensity = 0.1f;
    [SerializeField] private float sprayDistance = 5;

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
    }

    void Update()
    {
        if (_input.sprayButton)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, sprayDistance))
            {
                Paint(hit);
            }
        }

        if (_input.sprayButtonUp)
        {
            colorSpray = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }
    }

    void Paint(RaycastHit hit)
    {
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Prop"))
        {
            hit.transform.gameObject.GetComponent<Paintable>().Paint(colorSpray, sprayIntensity);
        }
    }
}
