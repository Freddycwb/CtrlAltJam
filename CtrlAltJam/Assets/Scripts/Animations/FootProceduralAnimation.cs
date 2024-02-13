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

    private void Start()
    {
        if (input == null && GetComponent<IInput>() != null)
        {
            _input = GetComponent<IInput>();
        }
        else if (input != null && input.GetComponent<IInput>() != null)
        {
            _input = input.GetComponent<IInput>();
        }

        if (planeMove != null)
        {
            planeMove.startMove += FirstStep;
            planeMove.endMove += ResetFoots;
        }

        rightFootTarget = rightFootRef.position;
        leftFootTarget = leftFootRef.position;

        rightStepStart = rightFootRef.position;
        leftStepStart = leftFootRef.position;

        currentRightStepTime = 1;
        currentLeftStepTime = 1;
    }

    private void SetVariables()
    {
        if (rb != null)
        {
            speed = Mathf.Clamp(new Vector2(rb.velocity.x, rb.velocity.z).magnitude, 1, float.PositiveInfinity);
        }

        if (currentRightStepTime < 1)
        {
            currentRightStepTime += Time.deltaTime / stepDuration * speed;
            if (currentRightStepTime >= 1)
            {
                if (endStep != null)
                {
                    endStep.Invoke();
                }
                if (endRightStep != null)
                {
                    endRightStep.Invoke();
                }
            }
        }

        if (currentLeftStepTime < 1)
        {
            currentLeftStepTime += Time.deltaTime / stepDuration * speed;
            if (currentLeftStepTime >= 1)
            {
                if (endStep != null)
                {
                    endStep.Invoke();
                }
                if (endLeftStep != null)
                {
                    endLeftStep.Invoke();
                }
            }
        }
    }

    private void SetFootPos()
    {
        Vector3 rightFootPos = Vector3.Lerp(rightStepStart, rightFootTarget, currentRightStepTime);
        rightFootRef.position = new Vector3(rightFootPos.x, rightFootPos.y + stepCurve.Evaluate(currentRightStepTime), rightFootPos.z);

        Vector3 leftFootPos = Vector3.Lerp(leftStepStart, leftFootTarget, currentLeftStepTime);
        leftFootRef.position = new Vector3(leftFootPos.x, leftFootPos.y + stepCurve.Evaluate(currentLeftStepTime), leftFootPos.z);
    }

    private void StepRoutine()
    {
        if (timeToNextStep <= 0)
        {
            if (Vector3.Distance(rightFootRef.position, transform.position + hips.right * footsDistance) > distToStep && rightStep)
            {
                if (startStep != null)
                {
                    startStep.Invoke();
                }
                if (startRightStep != null)
                {
                    startRightStep.Invoke();
                }
                Step(rightFootTarget, true);
            }
            else if (Vector3.Distance(leftFootRef.position, transform.position - hips.right * footsDistance) > distToStep && !rightStep)
            {
                if (startStep != null)
                {
                    startStep.Invoke();
                }
                if (startLeftStep != null)
                {
                    startLeftStep.Invoke();
                }
                Step(leftFootTarget, false);
            }
        }
        else
        {
            timeToNextStep -= Time.deltaTime;
        }
    }

    private void Update()
    {
        SetVariables();
        SetFootPos();
        StepRoutine();
    }

    private void Step(Vector3 foot, bool right)
    {
        Vector3 newFootPos = Vector3.zero;
        if (_input != null)
        {
            newFootPos = transform.position + new Vector3(_input.direction.x * stepDistance, transform.position.y, _input.direction.y * stepDistance);
        }
        else
        {
            newFootPos = transform.position;
        }

        Vector3.ClampMagnitude(newFootPos, distToStep);
        if (pathFinder.GetNavMeshClosestPos(newFootPos).magnitude - transform.position.magnitude <= stepDistance + footsDistance)
        {
            foot = pathFinder.GetNavMeshClosestPos(newFootPos);
        }
        else
        {
            foot = transform.position;
        }

        if (right)
        {
            rightStepStart = rightFootTarget;
            currentRightStepTime = 0;
            foot += hips.right * footsDistance;
            rightFootTarget = foot;
        }
        else
        {
            leftStepStart = leftFootTarget;
            currentLeftStepTime = 0;
            foot -= hips.right * footsDistance;
            leftFootTarget = foot;
        }
        timeToNextStep = minDelayBetweenSteps / speed;
        rightStep = !rightStep;
    }

    private void FirstStep()
    {
        Vector3 dir = Vector3.zero;
        if (_input != null)
        {
            new Vector3(_input.direction.normalized.x, transform.position.y, _input.direction.normalized.y);
        }

        float rightFootDist = Vector3.Distance(rightFootRef.position, (transform.position + hips.right * footsDistance) + dir);
        float leftFootDist = Vector3.Distance(leftFootRef.position, (transform.position - hips.right * footsDistance) + dir);

        Vector3 newFootPos = Vector3.zero;
        if (_input != null)
        {
            newFootPos = transform.position + new Vector3(_input.direction.x * stepDistance, transform.position.y, _input.direction.y * stepDistance);
        }
        Vector3.ClampMagnitude(newFootPos, distToStep);

        if (rightFootDist >= leftFootDist)
        {
            if (pathFinder.GetNavMeshClosestPos(newFootPos).magnitude - transform.position.magnitude <= stepDistance + footsDistance + dir.magnitude)
            {
                rightFootTarget = pathFinder.GetNavMeshClosestPos(newFootPos);
            }
            else
            {
                rightFootTarget = transform.position;
            }
            rightStepStart = rightFootRef.position;
            currentRightStepTime = 0;
            rightFootTarget += hips.right * footsDistance;
            rightStep = false;
        }
        else
        {
            if (pathFinder.GetNavMeshClosestPos(newFootPos).magnitude - transform.position.magnitude <= stepDistance + footsDistance + dir.magnitude)
            {
                leftFootTarget = pathFinder.GetNavMeshClosestPos(newFootPos);
            }
            else
            {
                leftFootTarget = transform.position;
            }
            leftStepStart = leftFootRef.position;
            currentLeftStepTime = 0;
            leftFootTarget -= hips.right * footsDistance;
            rightStep = true;
        }
    }

    private void ResetFoots()
    {
        if (Vector3.Distance(rightFootRef.position, transform.position + hips.right * footsDistance) > distToStep / 1.5f)
        {
            rightStepStart = rightFootRef.position;
            currentRightStepTime = 0;
            rightFootTarget = transform.position + hips.right * footsDistance;
        }
        if (Vector3.Distance(leftFootRef.position, transform.position + -hips.right * footsDistance) > distToStep / 1.5f)
        {
            leftStepStart = leftFootRef.position;
            currentLeftStepTime = 0;
            leftFootTarget = transform.position + -hips.right * footsDistance;
        }
    }

    private void OnDestroy()
    {
        if (planeMove != null)
        {
            planeMove.startMove -= FirstStep;
            planeMove.endMove -= ResetFoots;
        }
    }
}
