using UnityEngine;
using System.Collections;

public class PlayerMovement3D : UnityEngine.Networking.NetworkBehaviour {
	public float hunterStartCountdown = 2f;

    private float defaultSpeed = 0.1f;
    private float hunterSpeed = 0.2f;
	private float waterSpeedModifier = 0.5f;
	private float sprintingSpeedModifier = 2.5f;

	private bool inWater = false;
	private bool sprinting = false;

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
        if (!isLocalPlayer) {
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
        
        navAgent.Move(offset);
    }

	public void EnterWater() {
		inWater = true;
		spriteManager.EnterWater();
	}

	public void ExitWater() {
		inWater = false;
		spriteManager.ExitWater();
	}

	public void StartSprint() {
		sprinting = true;
	}

	public void StopSprint() {
		sprinting = false;
	}
}
