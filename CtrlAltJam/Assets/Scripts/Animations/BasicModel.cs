using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicModel : MonoBehaviour
{
    [SerializeField] private GameObject input;
    [SerializeField] private GameObject senses;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PathFinder pathFinder;
    [SerializeField] private PlaneMove planeMove;

    [SerializeField] private HeadProceduralAnimation viewTarget;
    [SerializeField] private PositionToDirection pivotTarget; 
    [SerializeField] private FootProceduralAnimation legs;
    [SerializeField] private RotateToDirectionOneAxis rightFoot;
    [SerializeField] private RotateToDirectionOneAxis leftFoot;

    private void Awake()
    {
        viewTarget.SetSuspicionManager(senses);
        viewTarget.SetInput(input);
        pivotTarget.SetInput(input);
        legs.SetPathFinder(pathFinder);
        legs.SetInput(input);
        legs.SetRigidbody(rb);
        legs.SetPlaneMove(planeMove);
        rightFoot.SetInput(input);
        leftFoot.SetInput(input);
    }

    private void OnDestroy()
    {
        if (rightFoot != null)
        {
            Destroy(rightFoot.gameObject);
        }
        if (leftFoot != null)
        {
            Destroy(leftFoot.gameObject);
        }
    }
}
