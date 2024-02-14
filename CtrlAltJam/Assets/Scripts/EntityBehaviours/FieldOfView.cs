using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float radius;
    [Range(0f, 360f)]
    [SerializeField] private float angle;

    [SerializeField] private GameObjectVariable targetRefVariable;
    [SerializeField] private GameObject targetRefGameObject;
    private Transform _targetTransform;
    private float _targetCenterY;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    [SerializeField] private float delayBetweenRoutine;

    private bool canSeeTarget;

    public Action targetEnterFOV;
    public Action targetExitFOV;

    public float GetRadius()
    {
        return radius;
    }

    public float GetAngle()
    {
        return angle;
    }

    public bool GetCanSeeTarget()
    {
        return canSeeTarget;
    }

    public Transform GetTargetTransform()
    {
        return _targetTransform;
    }

    public float GetTargetCenterY()
    {
        return _targetCenterY;
    }

    private void Start()
    {
        if (targetRefVariable != null)
        {
            _targetTransform = targetRefVariable.value.transform;
        }
        else if (targetRefGameObject != null)
        {
            _targetTransform = targetRefGameObject.transform;
        }
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenRoutine);
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position,radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 targetCenterPos = new Vector3(target.position.x, target.position.y + rangeChecks[0].bounds.center.y, target.position.z);
            Vector3 directionToTarget = (targetCenterPos - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetCenterPos);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    if (!canSeeTarget)
                    {
                        if (targetEnterFOV != null)
                        {
                            targetEnterFOV.Invoke();
                        }
                    }
                    canSeeTarget = true;
                }
                else
                {
                    TargetExitFOV();
                }
            }
            else
            {
                TargetExitFOV();
            }
        }
        else if (canSeeTarget)
        {
            TargetExitFOV();
        }
    }

    private void TargetExitFOV()
    {
        if (canSeeTarget)
        {
            if (targetExitFOV != null)
            {
                targetExitFOV.Invoke();
            }
        }
        canSeeTarget = false;
    }

    public bool CanSeeThis(Vector3 target)
    {
        bool canSeeThis = false;
        Vector3 targetCenterPos = new Vector3(target.x, target.y, target.z);
        Vector3 directionToTarget = (targetCenterPos - transform.position).normalized;
        if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetCenterPos);

            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                if (!canSeeTarget)
                {
                    if (targetEnterFOV != null)
                    {
                        targetEnterFOV.Invoke();
                    }
                }
                canSeeTarget = true;
            }
            else
            {
                TargetExitFOV();
            }
        }

        return canSeeThis;
    }
}
