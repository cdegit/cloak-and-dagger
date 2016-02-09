using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameEndManager : UnityEngine.Networking.NetworkBehaviour {

    // Do other objects make requests to this object for the game to end?
    // Does this object check if the game end conditions have been met? that'd be silly

    [ClientRpc]
    public void RpcSeekerVictory() {
        SceneManager.LoadScene("seekerVictory");
    }

    [ClientRpc]
    void RpcHunterVictory() {
        SceneManager.LoadScene("hunterVictory");
    }
}
