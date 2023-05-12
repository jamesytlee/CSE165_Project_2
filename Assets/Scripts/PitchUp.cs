using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchUp : MonoBehaviour
{
    public AircraftController aircraftController;
    public float pitchSpeed;

    public Material normalMaterial;     // normal material of button
    public Material pressedMaterial;    // pressed material of button

    private MeshRenderer meshRenderer;

    void Start()
    {
        // Get the MeshRenderer component
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the user's hand
        if (other.gameObject.CompareTag("PlayerHand"))
        {
            // Pitch the aircraft down
            aircraftController.PitchUp(pitchSpeed);

            // Change the button's material to the pressed material
            meshRenderer.material = pressedMaterial;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the user's hand
        if (other.gameObject.CompareTag("PlayerHand"))
        {
            // Change the button's material back to the normal material
            meshRenderer.material = normalMaterial;
        }
    }
}
