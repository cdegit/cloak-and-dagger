using UnityEngine;
using System.Collections;

public class PlayerMovement3D : UnityEngine.Networking.NetworkBehaviour {

    private float speedModifier = 0.4f;
    private NavMeshAgent navAgent;

    private SeekerBehaviour seeker;

    void FixedUpdate() {
        if (!isLocalPlayer || seeker.IsHiding()) {
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
        seeker = GetComponent<SeekerBehaviour>();

        Camera.main.GetComponent<followTarget3D>().target = transform;

        transform.position = new Vector3(0, 1, 0);
    }
}
