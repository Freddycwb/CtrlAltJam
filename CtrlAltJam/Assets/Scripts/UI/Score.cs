using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private FloatVariable score;
    [SerializeField] private TextMeshProUGUI tmp;

    private void Update()
    {
        tmp.text = score.value.ToString();
    }
}
