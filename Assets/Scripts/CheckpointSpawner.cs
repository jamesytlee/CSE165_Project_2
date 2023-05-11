using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CheckpointSpawner : MonoBehaviour
{
    [SerializeField] public GameObject checkpointSpherePrefab;
    [SerializeField] public TextAsset checkpointFile;

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
            Vector3 pos = new Vector3(float.Parse(coords[0]),
            float.Parse(coords[1]), float.Parse(coords[2]));
            
            // Message with parsed checkpoint coordinates
            Debug.Log(pos);
            
            positions.Add(pos / inchScale);
        }
        return positions;
    }
}
