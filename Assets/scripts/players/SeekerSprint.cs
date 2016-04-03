using UnityEngine;
using System.Collections;

public class SeekerSprint : MonoBehaviour {
	private float sprintDuration = 3f;
	private float sprintTimer = 0;
	private bool sprinting = false;
	private float sprintCooldownTime = 3f; // seconds
	private float sprintCooldownTimer = Mathf.Infinity;

	private PlayerIdentity id;
	private PlayerMovement3D movement;
	private InWorldUI ui;

	private NetworkedSFXManager sfxManager;

	void Start () {
		id = GetComponent<PlayerIdentity>();
		movement = GetComponent<PlayerMovement3D>();
		ui = GetComponentInChildren<InWorldUI>();
		sfxManager = GetComponentInChildren<NetworkedSFXManager>();
	}

	void Update () {
		if (id.IsHunter() || !id.IsThisPlayer()) {
			return;
		}

		if (sprintCooldownTimer >= sprintCooldownTime && Input.GetButtonUp("Fire2")) {
			sprinting = true;
			sprintCooldownTimer = 0;
			movement.StartSprint();
			sfxManager.PlaySprint();
		}

		if (sprinting) {
			if (sprintTimer < sprintDuration) {
				sprintTimer += Time.deltaTime;
			} else if (sprintTimer >= sprintDuration) {
				movement.StopSprint();
				sprintTimer = 0;
				sprinting = false;
			}
		} else {
			if (sprintCooldownTimer < sprintCooldownTime) {
				sprintCooldownTimer += Time.deltaTime;
			}
		}

		UIManager.instance.UpdateProgress((sprintCooldownTimer/sprintCooldownTime) * 100);
		ui.SetProgress((sprintCooldownTimer/sprintCooldownTime) * 100);
	}
}
