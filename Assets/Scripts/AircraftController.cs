using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AircraftController : MonoBehaviour
{
    [SerializeField] public Transform throttle;
    [SerializeField] public Transform rightHand;

    public float speedMultiplier = 15f;
    public float maxSpeed = 50f;

    private Vector3 handRotation; // initial rotation of the right hand
    private float updateInterval = 1f; // update every 1 seconds

    private void Start()
    {
        StartCoroutine(UpdateRotation());
    }

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

    IEnumerator UpdateRotation()
    {
        while (true)
        {
            // Hand to Aircraft Rotation
            // Store rotation of right hand
            handRotation = rightHand.eulerAngles;

            // Convert hand rotation to aircraft rotation
            Vector3 aircraftRotation = new Vector3(-handRotation.z, 0, handRotation.x);

            // Apply aircraft rotation
            transform.eulerAngles = aircraftRotation;

            // Wait for the defined interval before updating rotation again
            yield return new WaitForSeconds(updateInterval);
        }
    }
}
