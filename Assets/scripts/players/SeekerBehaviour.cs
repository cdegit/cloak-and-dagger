using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SeekerBehaviour : UnityEngine.Networking.NetworkBehaviour {
    private bool inHiding = false;

    private Renderer localRenderer;
    private Vector3 currentHidingPlace;
    private Vector3 defaultHidingPlace = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);

    void Start() {
        localRenderer = GetComponent<Renderer>();
    }

    [Command]
    public void CmdHide(Vector3 hidingPosition) {
        currentHidingPlace = hidingPosition;
        Debug.Log(currentHidingPlace);
        RpcHide(hidingPosition);
    }

    [ClientRpc]
    private void RpcHide(Vector3 hidingPosition) {
        // By making this an RPC, it gets executed on both clients
        // This will disable player movement
        inHiding = true;

        // This makes the player invisible
        // Should also make it so the other player can't run into them
        localRenderer.enabled = false;
        
        currentHidingPlace = hidingPosition;
    }

    [Command]
    public void CmdStopHiding() {
        RpcStopHiding();
    }

    [ClientRpc]
    private void RpcStopHiding() {
        inHiding = false;
        localRenderer.enabled = true;
        currentHidingPlace = defaultHidingPlace;
    }
    
    public void CmdCheckHidingPlace(Vector3 hidingPlace) {
        // Check if the Seeker is hiding in the same hiding place the Hunter is currently checking
        // There's no nice built in way to compare game objects, and they don't seem to want to serialize well anyways
        // So just compare the positions
        // If they are the same, kick the Seeker out of their hiding place
        if (currentHidingPlace.x == hidingPlace.x && currentHidingPlace.y == hidingPlace.y && currentHidingPlace.z == hidingPlace.z) {
            CmdStopHiding();
        }
    }

    public bool IsHiding() {
        return inHiding;
    }
}
