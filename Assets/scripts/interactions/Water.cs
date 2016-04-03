using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {
	private AudioSource splashing;
	private float originalVolume;
	private bool shouldPlayAudio;

	void Start() {
		splashing = GetComponent<AudioSource>();
		originalVolume = splashing.volume;
	}

	void Update() {
		if (shouldPlayAudio) {
			if (!splashing.isPlaying) {
				splashing.volume = originalVolume;
				splashing.Play();
			}
		} else {
			FadeSplashing();
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			PlayerMovement3D playerMovementManager = other.gameObject.GetComponent<PlayerMovement3D>();
			shouldPlayAudio = playerMovementManager.isMoving;
			playerMovementManager.EnterWater();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			shouldPlayAudio = false;
			other.gameObject.GetComponent<PlayerMovement3D>().ExitWater();
		}
	}

	void FadeSplashing() {
		float volume = splashing.volume;

		if (volume > 0.01) {
			volume -= Time.deltaTime;
			splashing.volume = volume;
		} else if (splashing.isPlaying) {
			splashing.Stop();
		}
	}
}
