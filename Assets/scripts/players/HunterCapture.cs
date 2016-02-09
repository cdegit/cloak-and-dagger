using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HunterCapture : MonoBehaviour {
    private bool isHunter;

	void Start () {
        isHunter = GetComponent<PlayerIdentity>().IsHunter();
	}

    bool validateStatus(Collider other) {
        PlayerIdentity otherId = other.gameObject.GetComponent<PlayerIdentity>();
        // If the current player is the hunter and the collider they've run into is the Seeker
        return isHunter && other.gameObject.tag == "Player" && otherId.IsSeeker() && !otherId.IsThisPlayer();
    }

    void OnTriggerEnter(Collider other) {
        if (validateStatus(other)) {
            // The Hunter is within range of the Seeker
            // TODO: Light up to show the user that they can act
        }
    }

    void OnTriggerStay(Collider other) {
        if (validateStatus(other)) {
            if (Input.GetButtonUp("Fire1")) {
                NetworkManager.singleton.ServerChangeScene("hunterVictory");
            }
        }
    }
}
