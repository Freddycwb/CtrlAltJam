using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeProp : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject[] props;

    private void Start()
    {
        Instantiate(props[Random.Range(0, props.Length)], spawnPoint.position, spawnPoint.rotation);
    }
}
