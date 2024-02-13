using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootProceduralAnimation : MonoBehaviour
{
    [SerializeField] private Transform rightFootRef;
    [SerializeField] private Transform leftFootRef;
    private Vector3 rightStepStart;
    private Vector3 leftStepStart;
    private Vector3 rightFootTarget;
    private Vector3 leftFootTarget;

    [SerializeField] private Transform hips;
    [SerializeField] private PathFinder pathFinder;
    [SerializeField] private GameObject input;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlaneMove planeMove;
    private IInput _input;

    private float speed = 1;

    [SerializeField] private float distToStep = 0.5f;
    [SerializeField] private float stepDistance = 0.75f;
    [SerializeField] private float footsDistance = 0.13f;
    
    private bool rightStep;
    [SerializeField] private float minDelayBetweenSteps = 0.5f;
    private float timeToNextStep;

    [SerializeField] private AnimationCurve stepCurve;
    private float currentRightStepTime;
    private float currentLeftStepTime;
    [SerializeField] private float stepDuration = 0.5f;


    public Action startStep;
    public Action endStep;

    public Action startRightStep;
    public Action endRightStep;

    public Action startLeftStep;
    public Action endLeftStep;

    public void SetPathFinder(PathFinder value)
    {
        pathFinder = value;
    }

    public void SetInput(GameObject value)
    {
        input = value;
    }

    public void SetRigidbody(Rigidbody value)
    {
        rb = value;
    }

    public void SetPlaneMove(PlaneMove value)
    {
        planeMove = value;
    }
}
