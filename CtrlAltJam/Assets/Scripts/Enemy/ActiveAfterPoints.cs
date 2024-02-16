using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveAfterPoints : MonoBehaviour
{
    [SerializeField] private FloatVariable score;
    [SerializeField] private float scoreToActive;
    [SerializeField] private UnityEvent action;

    private void Update()
    {
        if (score.value >= scoreToActive)
        {
            action.Invoke();
            Destroy(this);
        }
    }
}
