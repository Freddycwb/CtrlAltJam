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
    [SerializeField] private float delayAttack;
    [SerializeField] private bool isAttacking;


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
        if (_input != null && _input.batButtonDown && isAttacking == false)
        {
            StartCoroutine(DelayedAttack());
        }
    }

    private IEnumerator DelayedAttack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(delayAttack);
        Attack();
    }

    private void Attack()
    {
        Instantiate(damage, damageSpawnPoint.position, damageSpawnPoint.rotation);
        isAttacking = false;
    }
}
