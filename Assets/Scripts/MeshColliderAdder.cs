using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshColliderAdder : MonoBehaviour
{
    void Start()
    {
        // Get all child game objects
        foreach (Transform child in transform)
        {
            // If the child game object has a MeshFilter and no MeshCollider, add one
            if (child.GetComponent<MeshFilter>() != null && child.GetComponent<MeshCollider>() == null)
            {
                child.gameObject.AddComponent<MeshCollider>();
            }
        }
    }
}
