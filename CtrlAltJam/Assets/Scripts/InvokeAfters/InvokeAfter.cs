using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeAfter : MonoBehaviour
{
    [SerializeField] private UnityEvent action;
    [SerializeField] private GameObject[] instantiateOnAction;
    [SerializeField] private UnityEvent subAction;
    [SerializeField] private GameObject[] instantiateOnSubAction;

    public void CallAction()
    {
        InstantiateGameObjects(instantiateOnAction);
        action.Invoke();
    }

    public void CallSubAction()
    {
        InstantiateGameObjects(instantiateOnSubAction);
        subAction.Invoke();
    }

    private void InstantiateGameObjects(GameObject[] instantiate)
    {
        if (instantiate.Length > 0)
        {
            foreach (GameObject obj in instantiate)
            {
                Instantiate(obj, transform.position, transform.rotation);
            }
        }
    }
}
