using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionManager : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private FieldOfView fov;
    [SerializeField] private EnemyDirection enemyDirection;

    [SerializeField] private float distInfluence;

    private float _switchStateValue;
    [SerializeField] private float delayToDecreaseValue;
    private float _currentTimeToDecreaseValue;

    private Vector3 _playerLastPosition;

    [SerializeField] private float timeToGoLooking;
    [SerializeField] private float timeToGoChase;
    [SerializeField] private float timeToStopLooking;

    [SerializeField] private float timeStopInEachLookingPos;
    private float _currentTimeStopInLookingPos;

    public Action startLooking;
    public Action startChase;
    public Action checkLastPosition;

    public Action continueChase;
    public Action stopLooking;

    public Action loseSight;
    public Action changeLookingPosition;

    public Vector3 GetPlayerLastPosition()
    {
        return _playerLastPosition;
    }

    public void SetPlayerLastPosition(Vector3 pos)
    {
        _playerLastPosition = pos;
    }

    private void Start()
    {
        fov.targetEnterFOV += TargetEnterFOV;
        fov.targetExitFOV += TargetExitFOV;
        enemyDirection.reachDestination += CheckPlayerLastPos;
    }

    private void Update()
    {
        SuspicionMeter();
        LookingRoutine();
    }

    private void SuspicionMeter()
    {
        if (_currentTimeToDecreaseValue > 0)
        {
            _currentTimeToDecreaseValue -= Time.deltaTime;
            return;
        }
        if (fov.GetCanSeeTarget())
        {
            _playerLastPosition = fov.GetTargetTransform().position;
            _switchStateValue += Time.deltaTime / (1 + (Vector3.Distance(transform.position, fov.GetTargetTransform().position) * distInfluence));
        }
        switch (stateMachine.currentState)
        {
            case EnemyState.waiting:
                if (fov.GetCanSeeTarget())
                {
                    if (_switchStateValue >= timeToGoLooking)
                    {
                        _switchStateValue = 0;
                        if (startLooking != null)
                        {
                            startLooking.Invoke();
                        }
                    }
                }
                else
                {
                    if (_switchStateValue > 0)
                    {
                        _switchStateValue -= Time.deltaTime;
                        if (_switchStateValue < 0)
                        {
                            _switchStateValue = 0;
                        }
                    }
                }
                break;
            case EnemyState.looking:
                if (fov.GetCanSeeTarget())
                {
                    if (!enemyDirection.GetCheckingPlayerLastPos())
                    {
                        enemyDirection.SetCheckingPlayerLastPos(true);
                    }
                    if (_switchStateValue >= timeToGoChase)
                    {
                        _switchStateValue = 0;
                        if (startChase != null)
                        {
                            startChase.Invoke();
                        }
                    }
                }
                else if (_currentTimeStopInLookingPos < timeStopInEachLookingPos)
                {
                    _switchStateValue += Time.deltaTime;
                    if (_switchStateValue >= timeToStopLooking)
                    {
                        _switchStateValue = 0;
                        if (stopLooking != null)
                        {
                            stopLooking.Invoke();
                        }
                    }
                }
                break;
        }
    }

    public void RestartLookingTimer()
    {
        _currentTimeStopInLookingPos = 0;
    }

    private void LookingRoutine()
    {
        if (stateMachine.currentState != EnemyState.looking)
        {
            return;
        }
        if (_currentTimeStopInLookingPos < timeStopInEachLookingPos)
        {
            _currentTimeStopInLookingPos += Time.deltaTime;
            if (_currentTimeStopInLookingPos >= timeStopInEachLookingPos)
            {
                if (changeLookingPosition != null)
                {
                    changeLookingPosition.Invoke();
                }
            }
        }
    }

    public void Alert(Vector3 pos)
    {
        SetPlayerLastPosition(pos);
        if (stateMachine.currentState == EnemyState.waiting)
        {
            if (startLooking != null)
            {
                startLooking.Invoke();
            }
        }
    }

    private void TargetEnterFOV()
    {
        _switchStateValue = 0;
        _currentTimeToDecreaseValue = 0;
        if (stateMachine.currentState == EnemyState.chase)
        {
            if (continueChase != null)
            {
                continueChase.Invoke();
            }
        }
    }

    private void TargetExitFOV()
    {
        _switchStateValue = 0;
        _currentTimeToDecreaseValue = delayToDecreaseValue;
        if (stateMachine.currentState == EnemyState.chase)
        {
            if (checkLastPosition != null)
            {
                checkLastPosition.Invoke();
            }
        }
    }

    private void CheckPlayerLastPos()
    {
        if (enemyDirection.GetCheckingPlayerLastPos())
        {
            _switchStateValue = 0;
            if (loseSight != null)
            {
                loseSight.Invoke();
            }
        }
    }

    private void OnDestroy()
    {
        fov.targetEnterFOV -= TargetEnterFOV;
        fov.targetExitFOV -= TargetExitFOV;
        enemyDirection.reachDestination -= CheckPlayerLastPos;
    }
}
