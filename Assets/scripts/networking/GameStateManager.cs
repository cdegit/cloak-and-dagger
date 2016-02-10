using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// NOTE: Not currently being used, but may transition to this system later, so keeping it

public class GameStateManager : NetworkBehaviour {
    public static GameStateManager instance = null;

    private GameObject seeker;
    private GameObject hunter;

    private bool seekerIsHidden;
    private Vector3 currentHidingPlace;

    // should this send the command to clients for which one is the hunter???

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    [Command]
    public void CmdSeekerHide(Vector3 hidingPosition) {
        Debug.Log("Hiding the seeker...?");
        currentHidingPlace = hidingPosition;
        Debug.Log(hidingPosition);
        RpcSeekerHide(hidingPosition);
    }

    [ClientRpc]
    private void RpcSeekerHide(Vector3 hidingPosition) {
        // By making this an RPC, it gets executed on both clients
        // This will disable player movement
        seekerIsHidden = true;

        // This makes the player invisible
        // Should also make it so the other player can't run into them
        seeker.GetComponent<Renderer>().enabled = false;

        // It may be sufficient to compare if the position of each is the same, as there's no good built in way to compare these
        currentHidingPlace = hidingPosition;
    }
}
