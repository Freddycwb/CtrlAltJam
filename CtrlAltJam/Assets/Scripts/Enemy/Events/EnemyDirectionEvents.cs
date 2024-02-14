using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDirectionEvents : MonoBehaviour
{
    [SerializeField] private EnemyDirection enemyDirection;

    [SerializeField] private UnityEvent reachDestination;

    private void Start()
    {
        enemyDirection.reachDestination += ReachDestination;
    }

    private void ReachDestination()
    {
        reachDestination.Invoke();
    }

    private void OnDestroy()
    {
        enemyDirection.reachDestination -= ReachDestination;
    }
}
