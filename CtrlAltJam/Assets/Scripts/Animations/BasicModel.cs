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

    [SerializeField] private PositionToDirection masterTarget; 
    [SerializeField] private FootProceduralAnimation legs;
    [SerializeField] private RotateToDirectionOneAxis rightFoot;
    [SerializeField] private RotateToDirectionOneAxis leftFoot;

    private void Awake()
    {
        masterTarget.SetInput(input);
        legs.SetPathFinder(pathFinder);
        legs.SetInput(input);
        legs.SetRigidbody(rb);
        legs.SetPlaneMove(planeMove);
        rightFoot.SetInput(input);
        leftFoot.SetInput(input);
    }
}
