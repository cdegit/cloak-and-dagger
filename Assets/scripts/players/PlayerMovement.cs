using UnityEngine;
using System.Collections;

public class PlayerMovement : UnityEngine.Networking.NetworkBehaviour {

    private float speedModifier = 0.4f;

    void FixedUpdate() {
        if (!isLocalPlayer) {
            return;
        }

        float verticalSpeed = Input.GetAxis("Vertical");
        float horizontalSpeed = Input.GetAxis("Horizontal");

        Vector3 newPosition = transform.position;

        newPosition.x += horizontalSpeed * speedModifier;
        newPosition.y += verticalSpeed * speedModifier;

        transform.position = newPosition;
    }

    void Start() {
        if (!isLocalPlayer) {
            return;
        }

        Camera.main.GetComponent<followTarget>().target = transform;
    }
}