using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    public GameObject otherPlayer;
    public GameObject thisPlayer;
    public static PlayerManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

	void Update () {
        // The other player may not be available at Start, so do it on update until we can find them
        if (!otherPlayer) {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject go in gos) {
                if (go.GetComponent<PlayerIdentity>()) {
                    if (!go.GetComponent<PlayerIdentity>().IsThisPlayer()) {
                        otherPlayer = go;
                    } else {
                        thisPlayer = go;
                    }
                }
            }
        }
    }


}
