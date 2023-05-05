using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckpointSpawner : MonoBehaviour
{
    [SerializeField] public GameObject checkpointSpherePrefab;
    [SerializeField] public string filePath = "Assets/Scripts/sample_track.txt";

    void Start()
    {
        List<Vector3> checkpoints = ParseFile(filePath);

        for (int i = 0; i < checkpoints.Count; i++)
        {
            // Instantiate checkpoint sphere at parsed position
            GameObject checkpointSphere = Instantiate(checkpointSpherePrefab, checkpoints[i], Quaternion.identity);
            checkpointSphere.transform.SetParent(transform);
            checkpointSphere.name = "Checkpoint " + (i + 1);

            // Message with spawned checkpoint names
            Debug.Log(checkpointSphere.name);
        }
    }

    /*
     * Parses checkpoint text file and instantiates checkpoint sphere prefabs at coordinates.
     */
    List<Vector3> ParseFile(string filePath)
    {
        string content = LoadFileContent(filePath);
        if (content == null)
        {
            return new List<Vector3>();
        }

        List<Vector3> positions = new List<Vector3>();
        string[] lines = content.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] coords = lines[i].Split(' ');
            Vector3 pos = new Vector3(float.Parse(coords[0]),
            float.Parse(coords[1]), float.Parse(coords[2]));
            
            // Message with parsed checkpoint coordinates
            Debug.Log(pos);
            
            positions.Add(pos);
        }
        return positions;
    }

    /*
     * Loads file context in text format from the provided file path.
     */
    private string LoadFileContent(string filePath)
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        else
        {
            Debug.LogError("File not found: " + filePath);
            return null;
        }
    }

}
