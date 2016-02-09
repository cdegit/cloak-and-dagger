using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Podium : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PlayerIdentity id = other.gameObject.GetComponent<PlayerIdentity>();

            if (id.IsSeeker()) {
                NetworkManager.singleton.ServerChangeScene("seekerVictory");
            }
        }
    }
}
