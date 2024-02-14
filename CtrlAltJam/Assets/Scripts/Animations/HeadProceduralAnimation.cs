using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadProceduralAnimation : MonoBehaviour
{
    [SerializeField] private GameObject senses;
    private SuspicionManager _suspicionManager;
    [SerializeField] private GameObject input;
    private IInput _input;

    [SerializeField] private float moveVel;
    private bool lookingForward;

    public void SetSuspicionManager(GameObject value)
    {
        senses = value;
        if (senses.GetComponent<SuspicionManager>() != null)
        {
            _suspicionManager = senses.GetComponent<SuspicionManager>();
        }
    }

    public void SetInput(GameObject value)
    {
        input = value;
    }

    private void Start()
    {
        if (input != null && input.GetComponent<IInput>() != null)
        {
            _input = input.GetComponent<IInput>();
        }
        lookingForward = true;
        if (_suspicionManager != null)
        {
            _suspicionManager.startLooking += GoLookLastPosition;
            _suspicionManager.startChase += GoLookLastPosition;
            _suspicionManager.checkLastPosition += GoLookLastPosition;
            _suspicionManager.continueChase += GoLookLastPosition;
            _suspicionManager.stopLooking += GoLookForward;
            _suspicionManager.loseSight += GoLookForward;
        }
    }

    private void Update()
    {
        if (lookingForward)
        {
            if (_input != null && _input.direction.magnitude != 0)
            {
                transform.position = Vector3.Slerp(transform.position, senses.transform.position + new Vector3(_input.direction.x, 0, _input.direction.y), Time.deltaTime * moveVel);
            }
        }
        else if (_suspicionManager != null)
        {
            transform.position = _suspicionManager.GetPlayerLastPosition();
        }
    }

    private void GoLookForward()
    {
        lookingForward = true;
    }

    private void GoLookLastPosition()
    {
        lookingForward = false;
    }

    private void OnDestroy()
    {
        if (_suspicionManager != null)
        {
            _suspicionManager.startLooking -= GoLookLastPosition;
            _suspicionManager.startChase -= GoLookLastPosition;
            _suspicionManager.checkLastPosition -= GoLookLastPosition;
            _suspicionManager.continueChase -= GoLookLastPosition;
            _suspicionManager.stopLooking -= GoLookForward;
            _suspicionManager.loseSight -= GoLookForward;
        }
    }
}
