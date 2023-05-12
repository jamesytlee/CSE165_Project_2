using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class Checkpoint : MonoBehaviour
{
    public int ID;
    public Transform position;

    public Checkpoint(int id, Transform checkpointTransform)
    {
        ID = id;
        position = checkpointTransform;
    }
}
public class CheckpointSpawner : MonoBehaviour
{
    [SerializeField] public GameObject checkpointSpherePrefab;
    [SerializeField] public TextAsset checkpointFile;
    public Queue<Checkpoint> checkpointQueue = new Queue<Checkpoint>();
    private float inchScale = 39.3701f;

    void Start()
    {
        List<Vector3> checkpoints = ParseFile(checkpointFile);

        for (int i = 0; i < checkpoints.Count; i++)
        {
            // Instantiate checkpoint sphere at parsed position
            GameObject checkpointSphere = Instantiate(checkpointSpherePrefab, checkpoints[i], Quaternion.identity);
            checkpointSphere.transform.SetParent(transform);
            checkpointSphere.name = "Checkpoint " + (i + 1);

            // Add checkpoint references to queue
            Checkpoint checkpointRef = new Checkpoint(i + 1, checkpointSphere.transform);
            checkpointQueue.Enqueue(checkpointRef);

            // Message with spawned checkpoint names
            Debug.Log(checkpointSphere.name);
        }
    }

    /*
     * Parses checkpoint text file and instantiates checkpoint sphere prefabs at coordinates.
     */
    List<Vector3> ParseFile(TextAsset checkpointFile)
    {
        string content = checkpointFile.ToString();
        if (content == null)
        {
            return new List<Vector3>();
        }

        List<Vector3> positions = new List<Vector3>();
        string[] lines = content.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] coords = lines[i].Split(' ');
            Vector3 pos = new Vector3(float.Parse(coords[0]) / inchScale,
            float.Parse(coords[1]) / inchScale, float.Parse(coords[2]) / inchScale);
            
            // Message with parsed checkpoint coordinates
            Debug.Log(pos);
            
            positions.Add(pos);
        }
        return positions;
    }

    /*
     * Functions for manipulating checkpoint queue
     */
    public void EnqueueCheckpoint(Checkpoint checkpoint)
    {
        checkpointQueue.Enqueue(checkpoint);
    }

    public void DequeueCheckpoint()
    {
        if (checkpointQueue.Count == 0)
            throw new System.InvalidOperationException("Checkpoint Queue is Empty");

        Checkpoint passedCheckpoint = checkpointQueue.Dequeue();
        Debug.Log("Checkpoint " + passedCheckpoint.ID + " has been passed. Deleting it from queue.");
    }

    public Checkpoint PeekCheckpoint()
    {
        if (checkpointQueue.Count == 0)
            throw new System.InvalidOperationException("Checkpoint Queue is empty");

        return checkpointQueue.Peek();
    }
}
