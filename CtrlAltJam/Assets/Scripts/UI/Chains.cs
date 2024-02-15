using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chains : MonoBehaviour
{
    [SerializeField] private GameObject[] chains;
    private int chainsActive;


    public void Lock()
    {
        chains[chainsActive].SetActive(true);
        chainsActive++;
        Mathf.Clamp(0,chains.Length, chainsActive);
    }

    public void Free()
    {
        chainsActive--;
        chains[chainsActive].SetActive(false);
        Mathf.Clamp(0, chains.Length, chainsActive);
    }
}
