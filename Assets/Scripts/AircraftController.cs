using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{
    public Transform throttle;
    public float speedMultiplier = 8f;
    public float maxSpeed = 20f;

    private void Update()
    {
        // Get the x-axis rotation of the throttle
        float throttleRotation = throttle.localEulerAngles.x;
        
        if (throttleRotation > 180)
        {
            throttleRotation -= 360;
        }

        // Normalize the rotation value to the range of -1 to 1
        float normalizedRotation = throttleRotation / 180f;

        // Calculate the target speed based on the throttle rotation and speed multiplier
        float targetSpeed = normalizedRotation * speedMultiplier;

        // Limit the target speed to the maximum allowed speed
        targetSpeed = Mathf.Clamp(targetSpeed, -maxSpeed, maxSpeed);

        // Calculate the movement vector based on the target speed and the current frame's delta time
        Vector3 movement = transform.forward * targetSpeed * Time.deltaTime;

        // Apply the movement to the aircraft's Transform
        transform.Translate(movement, Space.World);
    }
}
