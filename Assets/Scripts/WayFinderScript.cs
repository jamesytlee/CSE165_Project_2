using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayFinderScript : MonoBehaviour
{
    public CheckpointSpawner checkpointManager;

    void Update()
    {
        Transform target = checkpointManager.PeekCheckpoint().transform;
        transform.LookAt(target);
    }
}
