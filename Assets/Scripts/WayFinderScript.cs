using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayFinderScript : MonoBehaviour
{
    public Transform currentCheckpoint; // The current checkpoint the object is heading towards
    public float distanceThreshold = 5f; // The minimum distance required to reach a checkpoint
    public GameObject[] checkpoints; // The array of all checkpoints in the scene with the "Checkpoint" tag

    // Start is called before the first frame update
    void Start()
    {
        // Find all game objects in the scene with the "Checkpoint" tag
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
    }

    // Update is called once per frame
    void Update()
    {
        // Find the nearest checkpoint to the object
        Transform nearestCheckpoint = GetNearestCheckpoint();

        // If the nearest checkpoint is different from the current checkpoint
        if (nearestCheckpoint != currentCheckpoint)
        {
            // Set the nearest checkpoint as the current checkpoint
            currentCheckpoint = nearestCheckpoint;
            Debug.Log("Heading towards checkpoint " + currentCheckpoint.name);
        }

        // Check if the object has reached the current checkpoint
        float distanceToCheckpoint = Vector3.Distance(transform.position, currentCheckpoint.position);
        if (distanceToCheckpoint < distanceThreshold)
        {
            Debug.Log("Reached checkpoint " + currentCheckpoint.name);

            // Set the next checkpoint as the current checkpoint
            int nextCheckpointIndex = (System.Array.IndexOf(checkpoints, currentCheckpoint.gameObject) + 1) % checkpoints.Length;
            currentCheckpoint = checkpoints[nextCheckpointIndex].transform;
            Debug.Log("Heading towards checkpoint " + currentCheckpoint.name);
        }
    }
    Transform GetNearestCheckpoint()
    {
        Transform nearestCheckpoint = null;
        float minDistance = Mathf.Infinity;

        // Find the nearest checkpoint to the object
        foreach (GameObject checkpoint in checkpoints)
        {
            float distance = Vector3.Distance(transform.position, checkpoint.transform.position);
            if (distance < minDistance)
            {
                nearestCheckpoint = checkpoint.transform;
                minDistance = distance;
            }
        }

        return nearestCheckpoint;
    }
}
