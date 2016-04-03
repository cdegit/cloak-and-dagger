using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkedSFXManager : NetworkBehaviour {
	public AudioClip echo;
	public AudioClip sprint;
	public AudioClip splashing;
	public AudioClip grass;
	public AudioClip steps;

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

		CheckWater(seekerMovement, seekerAudio);
		CheckWater(hunterMovement, hunterAudio);

		if (seekerMovement.isMoving) {
			// TODO: Play steps sound effect
		}
	}

	void CheckWater(PlayerMovement3D movement, AudioSource audio) {
		if (movement.inWater) {
			if (audio.clip != splashing) {
				audio.clip = splashing;
				audio.Play();
			}
		} else {
			if (audio.clip == splashing) {
				audio.Stop();
				audio.clip = null;
			}
		}
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
