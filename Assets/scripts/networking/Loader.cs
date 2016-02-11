using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
    public GameObject playerManager;
    public GameObject hunterAbilityUI;

    void Awake() {
        if (PlayerManager.instance == null) {
            Instantiate(playerManager);
        }

        if (HunterAbilityUI.instance == null) {
            Instantiate(hunterAbilityUI);
        }
    }
}
