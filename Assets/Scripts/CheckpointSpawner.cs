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
    public Material passedMaterial; // pressed material of checkpoint
    private float inchScale = 39.3701f;

    void Start()
    {
        List<Vector3> checkpoints = ParseFile(checkpointFile);

        for (int i = 0; i < checkpoints.Count; i++)
        {
            // Instantiate checkpoint sphere at parsed position
            GameObject checkpointSphere = Instantiate(checkpointSpherePrefab, checkpoints[i], Quaternion.identity);
            checkpointSphere.transform.SetParent(transform);
            int checkpointNumber = i + 1;
            checkpointSphere.name = checkpointNumber.ToString();

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

    public Checkpoint DequeueCheckpoint()
    {
        if (checkpointQueue.Count == 0)
            throw new System.InvalidOperationException("Checkpoint Queue is Empty");

        Checkpoint passedCheckpoint = checkpointQueue.Dequeue();
        Debug.Log("Checkpoint " + passedCheckpoint.ID + " has been passed. Deleting it from queue.");

        GameObject checkpointObject = GameObject.Find(passedCheckpoint.ID.ToString());

        // If the GameObject exists
        if (checkpointObject != null)
        {
            // Get the Renderer component 
            Renderer rend = checkpointObject.GetComponent<Renderer>();

            // If the Renderer exists
            if (rend != null)
            {
                // Change the material
                rend.material = passedMaterial;
            } 
            else
            {
                Debug.Log("Renderer not found on " + passedCheckpoint.ID);
            }
        } 
        else
        {
            Debug.Log("GameObject not found: " + passedCheckpoint.ID);
        }

        return passedCheckpoint;
    }

    public Checkpoint PeekCheckpoint()
    {
        if (checkpointQueue.Count == 0)
            throw new System.InvalidOperationException("Checkpoint Queue is empty");

        return checkpointQueue.Peek();
    }
}
