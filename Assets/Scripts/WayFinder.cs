using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayFinder : MonoBehaviour
{
    public CheckpointSpawner checkpointManager;

    void Update()
    {
        Transform target = checkpointManager.checkpointQueue.Peek().position;
        transform.LookAt(target);
    }
}
