using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HunterCapture : MonoBehaviour {
    private bool thisIsHunter;

	void Start () {
        PlayerIdentity id = GetComponent<PlayerIdentity>();
        thisIsHunter = id.IsHunter() && id.IsThisPlayer();
	}

    bool validateStatus(Collider other) {
        if (ReferenceEquals(other, null)) {
            return false;
        }

        PlayerIdentity otherId = other.gameObject.GetComponent<PlayerIdentity>();
        HidingManager hidingManager = other.gameObject.GetComponent<HidingManager>();

        if (ReferenceEquals(hidingManager, null)) {
            return false;
        }
        
        // If the hunter clicks they can take the seeker out of hiding
        // All of this shit needs to be synced to the server instead of just hopefully lining up

        // If the current player is the hunter and the collider they've run into is the Seeker and the Seeker isn't in hiding
        return thisIsHunter && other.gameObject.tag == "Player" && otherId.IsSeeker() && !otherId.IsThisPlayer() && !hidingManager.IsHiding();
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
