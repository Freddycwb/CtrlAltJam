using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private GameObject input;
    private IInput _input;
    [SerializeField] private GameObject damage;
    [SerializeField] private Transform damageSpawnPoint;


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
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (_input != null && _input.batButtonDown)
        {
            Instantiate(damage, damageSpawnPoint.position, Quaternion.identity);
        }
    }
}
