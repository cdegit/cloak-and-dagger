using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Gate : interactInRange {
	public NavMeshObstacle obstacle;

	private Collider player;

	private bool unlocking = false;
	private float unlockTime = 1f; // seconds
	private float unlockTimer = 0;

	private InWorldUI ui;

	void Start() {
		obstacle = GetComponent<NavMeshObstacle>();
		ui = GetComponentInChildren<InWorldUI>();
	}

	public override void Update() {
		base.Update();

		if (unlockTimer < unlockTime && unlocking) {
			ui.SetProgress((unlockTimer/unlockTime) * 100);
			unlockTimer += Time.deltaTime;
		}

		if (unlockTimer > unlockTime) {
			ui.SetProgress(100);
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
