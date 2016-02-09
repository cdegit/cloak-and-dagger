using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Podium : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerIdentity>().IsSeeker()) {
            NetworkManager.singleton.ServerChangeScene("seekerVictory");
        }
    }
}
