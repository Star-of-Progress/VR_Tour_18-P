using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script adds MeshCollider components to GameObjects with MeshFilters
public class MeshColider : MonoBehaviour
{
    [SerializeField] private bool includeSelf = true;
    // Serialized field to determine whether to include the parent object or just children
    // When true, the parent object will be included in the MeshCollider addition process

    void Start()
    {
        // Get all MeshFilter components in children (and optionally in parent)
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>(includeSelf);

        // Iterate through each found MeshFilter
        foreach (MeshFilter mf in meshFilters)
        {
            // Get the GameObject that has this MeshFilter
            GameObject obj = mf.gameObject;

            // Check if the GameObject doesn't already have a MeshCollider
            if (obj.GetComponent<MeshCollider>() == null)
            {
                // Add a MeshCollider component to the GameObject
                MeshCollider meshCollider = obj.AddComponent<MeshCollider>();
                // Assign the MeshFilter's mesh to the MeshCollider
                meshCollider.sharedMesh = mf.sharedMesh;
            }
        }
    }
}
