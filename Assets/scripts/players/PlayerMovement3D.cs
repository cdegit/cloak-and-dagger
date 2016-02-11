using UnityEngine;
using System.Collections;

public class PlayerMovement3D : UnityEngine.Networking.NetworkBehaviour {

    private float speedModifier = 0.4f;
    private NavMeshAgent navAgent;

    private HidingManager hidingManager;

    void FixedUpdate() {
        if (!isLocalPlayer) {
            return;
        }

        if (hidingManager.IsHiding()) {
            // Set the seeker's position to the position of whatever they're hiding in
            // This is mostly so that they'll move with whatever crowd they're in
            transform.position = hidingManager.currentHidingPlace.transform.position;
            return;
        }

        float verticalSpeed = Input.GetAxis("Vertical");
        float horizontalSpeed = Input.GetAxis("Horizontal");

        Vector3 offset = new Vector3(horizontalSpeed * speedModifier, 0, verticalSpeed * speedModifier);
        
        navAgent.Move(offset);
    }

    void Start() {
        if (!isLocalPlayer) {
            return;
        }

        navAgent = GetComponent<NavMeshAgent>();
        hidingManager = GetComponent<HidingManager>();

        Camera.main.GetComponent<followTarget3D>().target = transform;

        transform.position = new Vector3(0, 1, 0);
    }
}
