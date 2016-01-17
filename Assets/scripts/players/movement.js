#pragma strict

private var speedModifier = 0.4;

function FixedUpdate() {
    var verticalSpeed = Input.GetAxis("Vertical");
    var horizontalSpeed = Input.GetAxis("Horizontal");

    var newPosition = transform.position;

    newPosition.x += horizontalSpeed * speedModifier;
    newPosition.y += verticalSpeed * speedModifier;

    transform.position = newPosition;
}