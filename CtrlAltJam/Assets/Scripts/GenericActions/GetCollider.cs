using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCollider : MonoBehaviour
{
    [SerializeField] private MeshCollider collider;
    [SerializeField] private MeshFilter visual;

    void Start()
    {
        collider.sharedMesh = visual.mesh;
    }
}
