using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField] private FloatVariable score;

    
    void Start()
    {
        score.value = 0;    
    }


    public void AddScore(float points)
    {
        score.value += points;
    }
}
