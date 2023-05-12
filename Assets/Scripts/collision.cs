using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public GameObject player;
    public string collisionTag = "Collision";

    private CheckpointSpawner checkpointSpawner;

    void Start()
    {
        checkpointSpawner = FindObjectOfType<CheckpointSpawner>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(collisionTag))
        {
            StartCoroutine(MoveToCheckpoint());
        }
    }

    private IEnumerator MoveToCheckpoint()
    {
        yield return new WaitForSeconds(3f);

        if (checkpointSpawner.checkpointQueue.Count > 0)
        {
            Checkpoint previousCheckpoint = checkpointSpawner.PeekCheckpoint();

            player.transform.position = previousCheckpoint.transform.position;
        }
    }
}