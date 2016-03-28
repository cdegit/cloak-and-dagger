using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayAgain : NetworkBehaviour {

	public void go() {
		NetworkManager.singleton.ServerChangeScene("microLevelA");
	}
}
