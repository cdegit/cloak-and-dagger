using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HunterCapture : MonoBehaviour {
    private bool isHunter;

	void Start () {
        isHunter = GetComponent<PlayerIdentity>().IsHunter();
	}

    bool validateStatus(Collider other) {
        return isHunter && other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerIdentity>().IsSeeker();
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
