using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Instantiator : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform parent;

    public Action<GameObject> objCreated; 

    public void CreateObject()
    {
        GameObject a = Instantiate(obj, spawnPoint.position, spawnPoint.rotation);
        if (parent != null)
        {
            a.transform.SetParent(parent);
        }
        if (objCreated != null)
        {
            objCreated.Invoke(a);
        }
    }
}
