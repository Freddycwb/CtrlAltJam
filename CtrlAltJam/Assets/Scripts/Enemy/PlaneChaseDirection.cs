using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneChaseDirection : EnemyDirection
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private SuspicionManager suspicionManager;
    [SerializeField] private FieldOfView fov;
    private PathFinder _pathFinder;

    [SerializeField] private Transform[] patrolPoints;
    private int _currentPatrolPoint;

    [SerializeField] private float lookingRadius;
    [SerializeField] private float lookingMinRadius;
    private Vector3 _currentLookingPos;

    [SerializeField] private Vector2 followTime;
    private float _currentFollowTime;
    private bool _flanking;
    [SerializeField] private Vector2 flankTime;
    private float _currentFlankTime;

    [SerializeField] private float flankRadius;
    [SerializeField] private float flankMinRadius;
    private Vector3 _currentFlankPos;

    [SerializeField] private float distToFlank = 0.5f;
    [SerializeField] private float distToDestination = 0.2f;
    [SerializeField] private float distToCheckPlayerLastPos = 1;
    [SerializeField] private float distToPlayer = 2;

    protected Vector3 _startPos;

    private bool _goingToDestination;

    public void SetPatrolPoints(Transform[] newPatrolPoints)
    {
        patrolPoints = newPatrolPoints;
    }

    private void Awake()
    {
        _pathFinder = GetComponent<PathFinder>();
        _startPos = transform.position;
    }

    private void Start()
    {
        stateMachine.changedState += StateChanged;
        suspicionManager.changeLookingPosition += ChangeLookingPosition;
        if (stateMachine.currentState == EnemyState.chase)
        {
            _currentFollowTime = Random.Range(followTime.x, followTime.y);
        }
    }

    private void StateChanged(EnemyState state)
    {
        if (state == EnemyState.chase)
        {
            _checkingPlayerLastPos = false;
            _currentFollowTime = Random.Range(followTime.x, followTime.y);
        }
    }

    private void ChangeLookingPosition()
    {
        Vector3 newLookingPos = Vector3.zero;
        int a = 0;
        for (int i = 0; i < 1; i++)
        {
            a++;
            float x = Random.Range(-lookingRadius, lookingRadius);
            float y = Random.Range(-lookingRadius, lookingRadius);
            float z = Random.Range(-lookingRadius, lookingRadius);
            newLookingPos = suspicionManager.GetPlayerLastPosition() + new Vector3(x,y,z);
            if (Vector3.Distance(_pathFinder.GetNavMeshClosestPos(newLookingPos), suspicionManager.GetPlayerLastPosition()) < lookingMinRadius)
            {
                i--;
            }
            if (a > 100)
            {
                break;
            }
        }
        _currentLookingPos = newLookingPos;
    }

    private void Update()
    {
        FollowFlankLoop();
    }

    private void FollowFlankLoop()
    {
        if (stateMachine.currentState != EnemyState.chase)
        {
            return;
        }
        if (_currentFollowTime > 0)
        {
            _currentFollowTime -= Time.deltaTime;
            if (_currentFollowTime <= 0)
            {
                _currentFlankTime = Random.Range(flankTime.x, flankTime.y);
                _flanking = true;
                GoFlank();
            }
        }
        else
        {
            if (_currentFlankTime > 0)
            {
                _currentFlankTime -= Time.deltaTime;
                if (_currentFlankTime <= 0)
                {
                    _currentFollowTime = Random.Range(followTime.x, followTime.y);
                    _flanking = false;
                }
            }
        }
    }

    private void GoFlank()
    {
        Vector3 newFlankPos = Vector3.zero;
        int a = 0;
        for (int i = 0; i < 1; i++)
        {
            a++;
            float posX = Random.Range(-flankRadius, flankRadius);
            float posZ = Random.Range(-flankRadius, flankRadius);
            newFlankPos = new Vector3(posX, 0, posZ);
            if (Vector3.Distance(newFlankPos + fov.GetTargetTransform().position, fov.GetTargetTransform().position) < flankMinRadius)
            {
                i--;
            }
            if (a > 100)
            {
                break;
            }
        }
        _currentFlankPos = newFlankPos;
    }

    public override Vector2 Direction()
    {
        Vector3 dir = Vector3.zero;

        switch (stateMachine.currentState)
        {
            case EnemyState.waiting:
                if (patrolPoints.Length > 0)
                {
                    dir = CheckMargin(patrolPoints[_currentPatrolPoint].position, distToDestination);
                }
                else
                {
                    dir = CheckMargin(_startPos, distToDestination);
                }
                break;
            case EnemyState.chase:
                if (_checkingPlayerLastPos)
                {
                    dir = CheckMargin(_pathFinder.GetNavMeshClosestPos(suspicionManager.GetPlayerLastPosition()), distToCheckPlayerLastPos);
                }
                else
                {
                    if (_flanking)
                    {
                        dir = CheckMargin(fov.GetTargetTransform().position + _currentFlankPos, distToFlank);
                    }
                    else
                    {
                        dir = CheckMargin(fov.GetTargetTransform().position, distToFlank);
                    }
                }
                break;
            case EnemyState.looking:
                if (_checkingPlayerLastPos)
                {
                    dir = CheckMargin(_pathFinder.GetNavMeshClosestPos(suspicionManager.GetPlayerLastPosition()), distToCheckPlayerLastPos);
                }
                else
                {
                    dir = CheckMargin(_pathFinder.GetNavMeshClosestPos(_currentLookingPos), distToDestination);
                }
                break;
        }
        return new Vector2(dir.x, dir.z).normalized;
    }


    private Vector3 CheckMargin(Vector3 destination, float margin)
    {
        if (Vector3.Distance(transform.position, destination) > margin)
        {
            if (!_goingToDestination)
            {
                _goingToDestination = true;
                if (startMoveToDestination != null)
                {
                    startMoveToDestination.Invoke();
                }
            }
            return _pathFinder.GetDirectionTo(destination);
        }
        else
        {
            if (_goingToDestination)
            {
                if (_checkingPlayerLastPos)
                {
                    _currentLookingPos = suspicionManager.GetPlayerLastPosition();
                }
                _goingToDestination = false;
                if (reachDestination != null)
                {
                    reachDestination.Invoke();
                }
                _checkingPlayerLastPos = false;
                if (stateMachine.currentState == EnemyState.waiting)
                {
                    _currentPatrolPoint = _currentPatrolPoint + 1 >= patrolPoints.Length ? 0 : _currentPatrolPoint + 1;
                }
            }
            return Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        stateMachine.changedState -= StateChanged;
        suspicionManager.changeLookingPosition -= ChangeLookingPosition;
    }
}
