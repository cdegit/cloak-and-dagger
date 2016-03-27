using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Gate : interactInRange {
	public NavMeshObstacle obstacle;

	private Collider player;

	private bool unlocking = false;
	private float unlockTime = 1f; // seconds
	private float unlockTimer = 0;

	void Start() {
		obstacle = GetComponent<NavMeshObstacle>();
	}

	public override void Update() {
		base.Update();

		if (unlockTimer < unlockTime && unlocking) {
			unlockTimer += Time.deltaTime;
		}

		if (unlockTimer > unlockTime) {
			player.gameObject.GetComponent<GatePlayer>().UnlockGate(gameObject);
			unlockTimer = unlockTime;
		}
	}

	public override void SeekerInteraction(Collider thisPlayer) {
		// Start unlocking process
		unlocking = true;
		player = thisPlayer;
	}

	public override void HunterInteraction(Collider thisPlayer) {
		thisPlayer.gameObject.GetComponent<GatePlayer>().LockGate(gameObject);
	}
}
