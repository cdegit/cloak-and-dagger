using UnityEngine;
using System.Collections;

public class PlayerMovement3D : UnityEngine.Networking.NetworkBehaviour {

    private float defaultSpeedModifier = 0.1f;
    private float hunterSpeedModifier = 0.2f;

    private NavMeshAgent navAgent;
    private HidingManager hidingManager;
    private HunterEcho hunterEchoAbility;
    private PlayerIdentity id;

    void Start() {
        if (!isLocalPlayer) {
            return;
        }

        navAgent = GetComponent<NavMeshAgent>();
        hidingManager = GetComponent<HidingManager>();
        hunterEchoAbility = GetComponent<HunterEcho>();
        id = GetComponent<PlayerIdentity>();

        Camera.main.GetComponent<followTarget3D>().target = transform;
		GameObject.Find("Minimap Camera").GetComponent<followTarget3D>().target = transform;

        transform.position = new Vector3(0, 1, 0);
    }

	float GetCurrentSpeed() {
		if (id.IsHunter()) {
			return hunterSpeedModifier;
		}

		// If they're sneaking through the grass
		if (hidingManager.IsHidingMovable()) {
			return defaultSpeedModifier / 2;
		}

		return defaultSpeedModifier;
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

        if (!hunterEchoAbility.CanHunterMove()) {
            return;
        }

        float verticalSpeed = Input.GetAxis("Vertical");
        float horizontalSpeed = Input.GetAxis("Horizontal");

		float modifier = GetCurrentSpeed();

		Vector3 offset = new Vector3(horizontalSpeed * modifier, 0, verticalSpeed * modifier);
        
        navAgent.Move(offset);
    }
}
