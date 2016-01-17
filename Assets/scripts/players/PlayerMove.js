#pragma strict

public class PlayerMove extends UnityEngine.Networking.NetworkBehaviour {

    private var speedModifier = 0.4;

    function Update() {

        if (!isLocalPlayer) {
            return;
        }

        var verticalSpeed = Input.GetAxis("Vertical");
        var horizontalSpeed = Input.GetAxis("Horizontal");

        var newPosition = transform.position;

        newPosition.x += horizontalSpeed * speedModifier;
        newPosition.y += verticalSpeed * speedModifier;

        transform.position = newPosition;
    }
}