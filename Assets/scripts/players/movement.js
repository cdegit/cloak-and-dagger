#pragma strict

var speedModifier = 0.5;

function FixedUpdate () {
    var verticalSpeed = Input.GetAxis("Vertical");
    var horizontalSpeed = Input.GetAxis("Horizontal");

    var newPosition = transform.position;

    newPosition.x += horizontalSpeed;
    newPosition.y += verticalSpeed;

    transform.position = newPosition;
}