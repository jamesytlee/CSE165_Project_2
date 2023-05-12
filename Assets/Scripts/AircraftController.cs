using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{
    [SerializeField] public Transform throttle;
    public float speedMultiplier;
    public float maxSpeed;
    private bool isLocked;

    public CheckpointSpawner checkpointManager;
    private Transform lastCheckpoint;

    private void Start()
    {
        if (checkpointManager != null && checkpointManager.checkpointQueue.Count > 0)
        {
            lastCheckpoint = checkpointManager.checkpointQueue.Peek().position;
            StartCoroutine(Respawn(lastCheckpoint));
        }
        else
        {
            Debug.Log("Checkpoint Manager is not assigned or checkpointQueue is empty.");
        }
    }

    void Update()
    {
        if (isLocked)
        {
            return;
        }
        if (checkpointManager != null && checkpointManager.checkpointQueue.Count > 0)
        {
            lastCheckpoint = checkpointManager.checkpointQueue.Peek().position;
            Throttle();
        }
        else
        {
            Debug.Log("Checkpoint Manager is not assigned or checkpointQueue is empty.");
        }
    }

    /*
     * Function for aircraft respawn behavior
     */
    private IEnumerator Respawn(Transform respawnCheckpoint)
    {
        // Lock the aircraft in position
        isLocked = true;

        // Set the position
        transform.position = respawnCheckpoint.position;
        Debug.Log("The aircraft has spawned at checkpoint located at " + respawnCheckpoint.position);

        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);
        Debug.Log("3 seconds until aircraft can continue flight...");

        // Unlock the aircraft after 3 seconds
        isLocked = false;
    }

    /*
     * Function for handling child object collisions
     */
    public void HandleCheckpointCollision()
    {
        if (checkpointManager != null && checkpointManager.checkpointQueue.Count > 0)
        {
            checkpointManager.DequeueCheckpoint();
        }
    }

    /*
     * Function for Throttle
     */
    public void Throttle()
    {
        // Get the x-axis rotation of the throttle
        float throttleRotation = throttle.localEulerAngles.x;

        if (throttleRotation > 180)
        {
            throttleRotation -= 360;
        }
        float targetSpeed = 0;

        // If rotation is between 0 and 90, speed up
        if (throttleRotation <= 90 && throttleRotation > 0)
        {
            targetSpeed = (throttleRotation / 90f) * speedMultiplier;
        }
        // If rotation is between 0 and -90, speed down
        else if (throttleRotation >= -90 && throttleRotation < 0)
        {
            targetSpeed = (throttleRotation / 90f) * speedMultiplier;
        }

        // Limit the target speed to the maximum allowed speed
        targetSpeed = Mathf.Clamp(targetSpeed, -maxSpeed, maxSpeed);

        // Calculate the movement vector based on the target speed and the current frame's delta time
        Vector3 movement = transform.forward * targetSpeed * Time.deltaTime;

        // Apply the movement to the aircraft's Transform
        transform.Translate(movement, Space.World);
    }

    /*
     * Functions for Controlling Pitch, Yaw, and Roll
     */
    public void PitchUp (float speed)
    {
        // Change the x rotation to pitch the aircraft up
        transform.Rotate(speed, 0, 0);
    }

    public void PitchDown (float speed)
    {
        // Change the x rotation to pitch the aircraft down
        transform.Rotate(-speed, 0, 0);
    }

    public void YawRight (float speed)
    {
        // Change the y rotation to yaw the aircraft to the right
        transform.Rotate(0, speed, 0);
    }
    public void YawLeft(float speed)
    {
        // Change the y rotation to yaw the aircraft to the left
        transform.Rotate(0, -speed, 0);
    }

    public void RollRight(float speed)
    {
        // Change the z rotation to roll the aircraft to the right
        transform.Rotate(0, 0, -speed);
    }
    public void RollLeft(float speed)
    {
        // Change the z rotation to roll the aircraft to the left
        transform.Rotate(0, 0, speed);
    }
}
