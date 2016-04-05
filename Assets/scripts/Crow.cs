using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Crow : NetworkBehaviour {
	AudioSource cawing;
	Animator anim;

	void Start() {
		cawing = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other) {
		// if they're the seeker
		// start making noise and notify the hunter
		if (other.gameObject.GetComponent<PlayerIdentity>().IsSeeker()) {
			cawing.Play();
			anim.SetBool("isCawing", true);
			PlayerManager.instance.hunter.GetComponentInChildren<CrowIndicator>().SetTarget(transform);
		}
	}

	void OnTriggerExit(Collider other) {
		anim.SetBool("isCawing", false);
	}
}
