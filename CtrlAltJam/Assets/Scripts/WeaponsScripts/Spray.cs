using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spray : MonoBehaviour
{
    [SerializeField] private Color colorSpray;
    [SerializeField] public float sprayIntensity = 0.1f;

    [SerializeField] private GameObject input;
    private IInput _input;

    private bool pintando = false;

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
        pintando = _input.sprayButton;

        if (pintando)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
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
        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Paint"))
        {
            Renderer rend = hit.transform.GetComponent<Renderer>();
            if (rend == null)
            {
                Debug.LogError("Objeto não possui Renderer.");
                return;
            }

            rend.material.color = Color.Lerp(rend.material.color, colorSpray, Time.deltaTime * sprayIntensity);
        }
    }
}
