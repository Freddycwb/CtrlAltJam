using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    [SerializeField] private float delay;
    [SerializeField] private bool onStart = true;

    void Start()
    {
        if (onStart)
        {
            Delete();
        }
    }

    public void Delete()
    {
        StartCoroutine(DeleteRoutine(delay));
    }

    public void Delete(float time)
    {
        StartCoroutine(DeleteRoutine(time));
    }

    private IEnumerator DeleteRoutine(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
