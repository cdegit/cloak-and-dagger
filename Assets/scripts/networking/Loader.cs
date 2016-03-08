using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
    public GameObject playerManager;
	public GameObject uiManager;

    void Awake() {
        if (PlayerManager.instance == null) {
            Instantiate(playerManager);
        }

		if (UIManager.instance == null) {
			Instantiate(uiManager);
        }
    }
}
