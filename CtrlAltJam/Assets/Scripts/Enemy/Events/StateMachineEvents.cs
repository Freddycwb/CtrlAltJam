using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateMachineEvents : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;

    [SerializeField] private UnityEvent startWaiting;
    [SerializeField] private UnityEvent startLooking;
    [SerializeField] private UnityEvent startChase;

    private void Start()
    {
        stateMachine.changedState += StateChanged;
    }
    
    private void StateChanged(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.waiting:
                startWaiting.Invoke();
                break;
            case EnemyState.looking:
                startLooking.Invoke();
                break;
            case EnemyState.chase:
                startChase.Invoke();
                break;
        }
    }

    private void OnDestroy()
    {
        stateMachine.changedState -= StateChanged;
    }
}
