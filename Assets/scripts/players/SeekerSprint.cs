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

	void Start () {
		id = GetComponent<PlayerIdentity>();
		movement = GetComponent<PlayerMovement3D>();
	}

	void Update () {
		if (id.IsHunter() || !id.IsThisPlayer()) {
			return;
		}

		if (sprintCooldownTimer >= sprintCooldownTime && Input.GetButtonUp("Fire2")) {
			sprinting = true;
			sprintCooldownTimer = 0;
			movement.StartSprint();
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
	}
}
