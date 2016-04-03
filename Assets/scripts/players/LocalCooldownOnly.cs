using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LocalCooldownOnly : NetworkBehaviour {
	private bool disabled = false;

	void Update () {
		if (disabled) {
			return;
		}

		if (!isLocalPlayer) {
			transform.FindChild("CooldownCanvas").gameObject.SetActive(false);
		}
	}
}
