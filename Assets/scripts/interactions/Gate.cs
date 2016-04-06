using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Gate : interactInRange {
	public NavMeshObstacle obstacle;

	private AudioSource audio;
	private Collider player;

	private bool unlocking = false;
	private float unlockTime = 1f; // seconds
	private float unlockTimer = 0;

	private InWorldUI ui;
	private bool locked;

	void Start() {
		obstacle = GetComponent<NavMeshObstacle>();
		ui = GetComponentInChildren<InWorldUI>();
		audio = GetComponent<AudioSource>();
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
			locked = false;
			unlockTimer = unlockTime;
		}
	}

	public override void SeekerInteraction(Collider thisPlayer) {
		// Start unlocking process
		unlocking = true;
		player = thisPlayer;
		audio.Play();
	}

	public override void HunterInteraction(Collider thisPlayer) {
		if (locked) {
			thisPlayer.gameObject.GetComponent<GatePlayer>().UnlockGate(gameObject);
			locked = false;
			audio.Play();
		} else {
			thisPlayer.gameObject.GetComponent<GatePlayer>().LockGate(gameObject);
			locked = true;
		}
	}
}
