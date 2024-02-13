using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicModel : MonoBehaviour
{
    [SerializeField] private GameObject input;
    [SerializeField] private GameObject senses;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PathFinder pathFinder;
    [SerializeField] private PlaneMove planeMove;

    [SerializeField] private FootProceduralAnimation legs;

    private void Awake()
    {

    }
}
