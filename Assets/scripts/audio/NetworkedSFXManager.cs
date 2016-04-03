using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkedSFXManager : NetworkBehaviour {
	public AudioClip echo;
	public AudioClip sprint;
	public AudioClip splashing;
	public AudioClip onGrass;
	public AudioClip inGrass;
	public AudioClip walkingSlowly;
	public AudioClip walkingQuickly;

	private bool doCrossfade;
	private float crossFadeModifier;

	void Update() {
		GameObject hunter = PlayerManager.instance.hunter;
		GameObject seeker = PlayerManager.instance.seeker;

		if (!seeker || !hunter) {
			return;
		}

		PlayerMovement3D hunterMovement = hunter.GetComponent<PlayerMovement3D>();
		PlayerMovement3D seekerMovement = seeker.GetComponent<PlayerMovement3D>();

		AudioSource hunterAudio = hunter.GetComponent<AudioSource>();
		AudioSource seekerAudio = seeker.GetComponent<AudioSource>();

		if (CheckState(seekerMovement.inWater, seekerAudio, splashing)) return;
		if (CheckState(hunterMovement.inWater, hunterAudio, splashing)) return;

		if (CheckState(seekerMovement.onGrass, seekerAudio, onGrass)) return;
		if (CheckState(seekerMovement.inGrass, seekerAudio, inGrass)) return;

		if (CheckState(seekerMovement.sprinting, seekerAudio, walkingQuickly)) return;
		if (CheckState(seekerMovement.isMoving, seekerAudio, walkingSlowly)) return;
	}

	bool CheckState(bool state, AudioSource audio, AudioClip clip) {
		if (state) {
			if (audio.clip != clip) {
				audio.clip = clip;
				audio.Play();
			}
		} else {
			if (audio.clip == clip) {
				audio.Stop();
				audio.clip = null;
			}
		}

		return state;
	}

	public void PlayEcho() {
		CmdPlayEcho();
	}

	[Command]
	private void CmdPlayEcho() {
		RpcPlayEcho();
	}

	[ClientRpc]
	private void RpcPlayEcho() {
		PlayerManager.instance.hunter.GetComponent<AudioSource>().PlayOneShot(echo);
	}


	public void PlaySprint() {
		CmdPlaySprint();
	}

	[Command]
	private void CmdPlaySprint() {
		RpcPlaySprint();
	}

	[ClientRpc]
	private void RpcPlaySprint() {
		PlayerManager.instance.hunter.GetComponent<AudioSource>().PlayOneShot(sprint);
	}
}
