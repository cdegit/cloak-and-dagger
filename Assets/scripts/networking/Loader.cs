using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
    public GameObject playerManager;

    void Awake() {
        if (PlayerManager.instance == null) {
            Instantiate(playerManager);
        }
    }
}
