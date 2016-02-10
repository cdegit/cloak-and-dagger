using UnityEngine;
using System.Collections;

// NOTE: Not currently used, as we're not currently using the gameStateManager

public class Loader : MonoBehaviour {
    public GameObject gameStateManager;

    void Awake() {
        if (GameStateManager.instance == null) {
            Instantiate(gameStateManager);
        }
    }
}
