using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFinder : MonoBehaviour
{
    protected NavMeshPath _navMeshPath;

    [SerializeField] private float maxDistFromDestination = 50;

    private void Awake()
    {
        _navMeshPath = new NavMeshPath();
    }

    public Vector3 GetDirectionTo(Vector3 destination)
    {
        Vector3 dir = Vector3.zero;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(destination, out hit, maxDistFromDestination, NavMesh.AllAreas))
        {
            NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, _navMeshPath);
            dir = _navMeshPath.corners.Length >= 2 ? _navMeshPath.corners[1] - transform.position : Vector3.zero;
        }
        return dir;
    }

    public Vector3 GetNavMeshClosestPos(Vector3 destination)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(destination, out hit, maxDistFromDestination, NavMesh.AllAreas);
        return hit.position;
    }
}
