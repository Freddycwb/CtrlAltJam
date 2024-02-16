using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private InvokeAfterTimer invokeAfter;

    private void Update()
    {
        tmp.text = invokeAfter.GetTimePassed().ToString("F0");
    }
}
