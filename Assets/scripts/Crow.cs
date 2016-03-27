using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Crow : NetworkBehaviour {
	AudioSource cawing;

	void Start() {
		cawing = GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider other) {
		// if they're the seeker
		// start making noise and notify the hunter
		if (other.gameObject.GetComponent<PlayerIdentity>().IsSeeker()) {
			cawing.Play();
			PlayerManager.instance.hunter.GetComponentInChildren<CrowIndicator>().SetTarget(transform);
		}
	}
}
