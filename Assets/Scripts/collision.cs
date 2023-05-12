using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    private bool isWaiting = false;
    private float waitStartTime = 0f;
    [SerializeField] public CheckpointSpawner checkpointSpawner;


    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.CompareTag("collision") && !isWaiting)
        {
            isWaiting = true;
            waitStartTime = Time.time;
        }
    }

    void Update()
    {
        if (isWaiting && Time.time >= waitStartTime + 3.0f)
        {
            Debug.Log("Waiting for move...");
            isWaiting = false;
            transform.position = checkpointSpawner.checkpointQueue.Peek().transform.position;
        }
    }
}
