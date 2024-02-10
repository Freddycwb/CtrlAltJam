using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variable<T> : ScriptableObject
{
    public T value;

    public bool persistBetweenScenes = false;

    private void OnEnable()
    {
        if (persistBetweenScenes)
        {
            hideFlags = HideFlags.DontUnloadUnusedAsset;
        }
    }

    public void SetValue(T value)
    {
        this.value = value;
    }

    public void SetValue(Variable<T> value)
    {
        this.value = value.value;
    }
}
