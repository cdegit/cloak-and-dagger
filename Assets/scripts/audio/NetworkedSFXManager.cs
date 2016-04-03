using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkedSFXManager : NetworkBehaviour {
	public AudioSource echo;
	public AudioSource sprint;

	public void PlayEcho() {
		CmdPlayEcho();
	}

	[Command]
	private void CmdPlayEcho() {
		RpcPlayEcho();
	}

	[ClientRpc]
	private void RpcPlayEcho() {
		AudioSource.PlayClipAtPoint(echo.clip, PlayerManager.instance.hunter.transform.position);
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
		AudioSource.PlayClipAtPoint(sprint.clip, PlayerManager.instance.seeker.transform.position);
	}
}
