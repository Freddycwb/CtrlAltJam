using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private GameObject input;
    private IInput _input;

    private float xRotation = 0f;
    private float yRotation = 0f;

    [SerializeField] private Vector3 offset;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        _input = input.GetComponent<IInput>();
    }

    private void Update()
    {
        xRotation -= _input.look.x;
        xRotation = Mathf.Clamp(xRotation, -135f, 135f);

        yRotation += _input.look.y;
        transform.eulerAngles = new Vector3(xRotation * _input.sensitivity.x, yRotation * _input.sensitivity.y, 0f) + offset;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
