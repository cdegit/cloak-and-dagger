using UnityEngine;
using System.Collections;

public class BasketAnimEvent : MonoBehaviour {
	public void OutOfBasket() {
		PlayerIdentity id = PlayerManager.instance.thisPlayer.GetComponent<PlayerIdentity>();

		if (id.IsSeeker()) {
			PlayerManager.instance.thisPlayer.GetComponent<HidingManager>().CmdFinishedAnimation();
		}
	}
}
