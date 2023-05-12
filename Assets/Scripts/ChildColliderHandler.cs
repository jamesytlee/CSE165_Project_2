using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildColliderHandler : MonoBehaviour
{
    public AircraftController aircraft;

    private void Start()
    {
        // Assuming the parent GameObject has the AircraftController script
        aircraft = GetComponentInParent<AircraftController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Checkpoint"))
        {
            aircraft.HandleCheckpointCollision();
        }
    }
}
