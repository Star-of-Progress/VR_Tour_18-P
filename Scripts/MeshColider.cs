using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColider : MonoBehaviour
{
    [SerializeField] private bool includeSelf = true;

    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>(includeSelf);

        foreach (MeshFilter mf in meshFilters)
        {
            GameObject obj = mf.gameObject;

            if (obj.GetComponent<MeshCollider>() == null)
            {
                MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = mf.sharedMesh;
            }
        }
    }
}
