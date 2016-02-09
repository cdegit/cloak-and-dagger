using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Podium : MonoBehaviour {
    public GameObject gameEndManager;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PlayerIdentity>().IsSeeker()) {
            // Tell the server to tell both players that the seeker has won
            gameEndManager.GetComponent<GameEndManager>().RpcSeekerVictory();
        }
    }
}
