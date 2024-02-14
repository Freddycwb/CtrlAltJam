using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckSolutions : MonoBehaviour
{
    private bool _stuck;

    [SerializeField] private EnemyDirection enemyDirection;
    private bool _goingToDestination;
    [SerializeField] private PlaneMove planeMove;
    private bool _havePlaneMove;

    [SerializeField] private float distToStuck = 0.5f;
    [SerializeField] private float timeToStuck = 1;
    private float _currentTimeToStuck;
    private Vector3 randomDir;

    private Vector3 lastPos;

    private void Start()
    {
        _havePlaneMove = planeMove != null;
        enemyDirection.startMoveToDestination += StartMoveToDestination;
        enemyDirection.reachDestination += ReachDestination;
    }

    private void StartMoveToDestination()
    {
        _goingToDestination = true;
    }

    private void ReachDestination()
    {
        _goingToDestination = false;
    }

    private void Update()
    {
        CheckIfStuck();
        if (_stuck)
        {
            if (_havePlaneMove)
            {
                planeMove.Move(randomDir);
            }
        }
    }

    private void CheckIfStuck()
    {
        bool planeMoveStuck = (!_havePlaneMove || (_havePlaneMove && _goingToDestination));
        if (Vector3.Distance(transform.position, lastPos) <= distToStuck && planeMoveStuck)
        {
            _currentTimeToStuck += Time.deltaTime;
            if (_currentTimeToStuck >= timeToStuck && !_stuck)
            {
                randomDir = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                _stuck = true;
            }
            else
            {
                lastPos = transform.position;
                _currentTimeToStuck = 0;
                _stuck = false;
            }
        }
    }
}
