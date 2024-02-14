using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState startState;
    public EnemyState currentState { get; private set; }
    public Action<EnemyState> changedState;

    private void Start()
    {
        SetState(startState);
    }

    private void SetState(EnemyState value)
    {
        currentState = value;
        if (changedState != null)
        {
            changedState.Invoke(currentState);
        }
    }

    public void GoWait()
    {
        SetState(EnemyState.waiting);
    }
    public void GoLook()
    {
        SetState(EnemyState.looking);
    }
    public void GoChase()
    {
        SetState(EnemyState.chase);
    }
}
