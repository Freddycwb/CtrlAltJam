using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToDirection : MonoBehaviour
{
    [SerializeField] private GameObject input;
    private IInput _input;
    [SerializeField] private float moveVel;
    [SerializeField] private Vector3 offset;

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
    }

    private void FixedUpdate()
    {
        if (_input == null)
        {
            return;
        }
        Move();
    }

    private void Move()
    {
        if (_input.direction.magnitude != 0)
        {
            transform.position = Vector3.Slerp(transform.position, input.transform.position + new Vector3(_input.direction.x, 0, _input.direction.y), Time.deltaTime * moveVel);
        }
        transform.position += offset;
    }
}
