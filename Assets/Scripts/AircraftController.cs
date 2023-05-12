using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{
    [SerializeField] public Transform throttle;
    [SerializeField] public Transform rightHand;

    public float speedMultiplier = 15f;
    public float maxSpeed = 50f;

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

    // Functions for Controlling Pitch
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

    // Functions for Controlling Roll
    public void RollRight (float speed)
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
