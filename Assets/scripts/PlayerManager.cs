using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {
    public GameObject otherPlayer;
    public GameObject thisPlayer;
	public GameObject hunter;
	public GameObject seeker;
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
        if (!otherPlayer || !thisPlayer) {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag("Player");

            foreach (GameObject go in gos) {
				PlayerIdentity id = go.GetComponent<PlayerIdentity>();
                if (id) {
                    if (!id.IsThisPlayer()) {
                        otherPlayer = go;
                    } else {
                        thisPlayer = go;
                    }

					if (id.IsHunter()) {
						hunter = go;
					} else {
						seeker = go;
					}
                }
            }
        }
    }


}
