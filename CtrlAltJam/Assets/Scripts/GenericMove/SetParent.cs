using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParent : MonoBehaviour
{
    [SerializeField] private GameObjectVariable targetGameObjectVariable;
    [SerializeField] private GameObject targetGameObject;
    private Transform _targetTransform;

    [SerializeField] private bool setParentOnStart = true;

    void Start()
    {
        if (targetGameObjectVariable != null && targetGameObjectVariable.value != null)
        {
            _targetTransform = targetGameObjectVariable.value.transform;
        }
        else if (targetGameObject != null)
        {
            _targetTransform = targetGameObject.transform;
        }

        if (setParentOnStart && _targetTransform != null)
        {
            transform.SetParent(_targetTransform);
        }
    }
}
