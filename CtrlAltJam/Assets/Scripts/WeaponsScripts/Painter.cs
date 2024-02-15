using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public Color colorSpray;
    public float sprayIntensity;

    private void OnTriggerStay(Collider other)
    {
       other.GetComponent<Paintable>().Paint(colorSpray, sprayIntensity);
    }
}
