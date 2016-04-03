using UnityEngine;
using System.Collections;

public class PlayerMovement3D : UnityEngine.Networking.NetworkBehaviour {
	public float hunterStartCountdown = 2f;
	public bool isMoving = false;

    private float defaultSpeed = 0.1f;
    private float hunterSpeed = 0.2f;
	private float waterSpeedModifier = 0.5f;
	private float sprintingSpeedModifier = 2.5f;

	public bool inWater = false;
	public bool sprinting = false;
	public bool onGrass = false;
	public bool inGrass = false;

    private NavMeshAgent navAgent;
    private HidingManager hidingManager;
    private HunterEcho hunterEchoAbility;
    private PlayerIdentity id;
	private SpriteFollowPlayer spriteManager;

    void Start() {
        if (!isLocalPlayer) {
            return;
        }

        navAgent = GetComponent<NavMeshAgent>();
        hidingManager = GetComponent<HidingManager>();
        hunterEchoAbility = GetComponent<HunterEcho>();
        id = GetComponent<PlayerIdentity>();
		spriteManager = GetComponent<SpriteFollowPlayer>();

        Camera.main.GetComponent<followTarget3D>().target = transform;
		GameObject.Find("Minimap Camera").GetComponent<followTarget3D>().target = transform;
    }

	float GetCurrentSpeed() {
		float currentSpeed = defaultSpeed;

		if (id.IsHunter()) {
			currentSpeed = hunterSpeed;
		}

		// If they're sneaking through the grass
		if (hidingManager.IsHidingMovable()) {
			currentSpeed = defaultSpeed / 2;
		}

		if (inWater) {
			currentSpeed *= waterSpeedModifier;
		}

		if (sprinting) {
			currentSpeed *= sprintingSpeedModifier;
		}

		return currentSpeed;
	}

    void FixedUpdate() {
		isMoving = false;

		if (!isLocalPlayer || !navAgent.isOnNavMesh) {
            return;
        }

		// On game start, stop the Hunter from moving for a couple of seconds

        if (hidingManager.IsHidingStationary()) {
            // Set the seeker's position to the position of whatever they're hiding in
            // This is mostly so that they'll move with whatever crowd they're in
            transform.position = hidingManager.currentHidingPlace.transform.position;
            return;
        }

        if (!hunterEchoAbility.CanHunterMove()) {
            return;
        }

		if (id.IsHunter() && hunterStartCountdown > 0) {
			hunterStartCountdown -= Time.deltaTime;
			return;
		}

        float verticalSpeed = Input.GetAxis("Vertical");
        float horizontalSpeed = Input.GetAxis("Horizontal");

		float modifier = GetCurrentSpeed();

		Vector3 offset = new Vector3(horizontalSpeed * modifier, 0, verticalSpeed * modifier);
		Vector3 zeroVector = new Vector3(0, 0, 0);
		float percentageDifferenceAllowed = 0.001f;

		// Compare vectors using their distance to avoid floating precision errors
		if ((offset - zeroVector).sqrMagnitude >= percentageDifferenceAllowed) {
			isMoving = true;
		}
        
        navAgent.Move(offset);
    }

	public void EnterWater() {
		inWater = true;

		if (spriteManager) {
			spriteManager.EnterWater();
		}
	}

	public void ExitWater() {
		inWater = false;

		if (spriteManager) {
			spriteManager.ExitWater();
		}
	}

	public void StartSprint() {
		sprinting = true;
	}

	public void StopSprint() {
		sprinting = false;
	}
}
