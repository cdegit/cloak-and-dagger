using UnityEngine;
using System.Collections;

public class PlayerMovement3D : UnityEngine.Networking.NetworkBehaviour {

    private float speedModifier = 0.1f;
    private float defaultSpeedModifier = 0.1f;
    private float hunterSpeedModifier = 0.2f;

    private NavMeshAgent navAgent;
    private HidingManager hidingManager;
    private HunterEcho hunterEchoAbility;
    private SpriteFollowPlayer sprite;
    private PlayerIdentity id;

    private float lastAngle = 0;

    void Start() {
        if (!isLocalPlayer) {
            return;
        }

        navAgent = GetComponent<NavMeshAgent>();
        hidingManager = GetComponent<HidingManager>();
        hunterEchoAbility = GetComponent<HunterEcho>();
        sprite = GetComponent<SpriteFollowPlayer>();
        id = GetComponent<PlayerIdentity>();

        Camera.main.GetComponent<followTarget3D>().target = transform;

        transform.position = new Vector3(0, 1, 0);
    }

    void FixedUpdate() {
        if (!isLocalPlayer) {
            return;
        }

        if (hidingManager.IsHidingStationary()) {
            // Set the seeker's position to the position of whatever they're hiding in
            // This is mostly so that they'll move with whatever crowd they're in
            transform.position = hidingManager.currentHidingPlace.transform.position;
            return;
        }

        // If they're sneaking through the grass
        if (hidingManager.IsHidingMovable()) {
            speedModifier = defaultSpeedModifier / 4;
        } else {
            speedModifier = defaultSpeedModifier;
        }

        if (id.IsHunter()) {
            speedModifier = hunterSpeedModifier;
        } else {
            speedModifier = defaultSpeedModifier;
        }

        if (!hunterEchoAbility.CanHunterMove()) {
            return;
        }

        float verticalSpeed = Input.GetAxis("Vertical");
        float horizontalSpeed = Input.GetAxis("Horizontal");

        Vector3 offset = new Vector3(horizontalSpeed * speedModifier, 0, verticalSpeed * speedModifier);
        
        navAgent.Move(offset);

        DetermineAnimation(offset);
    }

    void DetermineAnimation(Vector3 offset) {
        float newMovementAngle = Vector3.Angle(offset, transform.right);
        float velocity = Vector3.Magnitude(offset);

        if (offset.z < 0) {
            newMovementAngle = 360 - newMovementAngle;
        }

        if (velocity == 0) {
            newMovementAngle = lastAngle;
        }

        sprite.SetAnimationParams(velocity, newMovementAngle);
        lastAngle = newMovementAngle;
    }
    
}
