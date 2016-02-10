using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SeekerBehaviour : UnityEngine.Networking.NetworkBehaviour {
    public bool inHiding = false;

    private Renderer localRenderer;

    void Start() {
        localRenderer = GetComponent<Renderer>();
    }

    [Command]
    public void CmdHide() {
        RpcHide();
    }

    [ClientRpc]
    private void RpcHide() {
        // By making this an RPC, it gets executed on both clients
        // This will disable player movement
        inHiding = true;

        // This makes the player invisible
        // Should also make it so the other player can't run into them
        localRenderer.enabled = false;
    }

    [Command]
    public void CmdStopHiding() {
        RpcStopHiding();
    }

    [ClientRpc]
    private void RpcStopHiding() {
        inHiding = false;
        localRenderer.enabled = true;
    }

    public bool IsHiding() {
        return inHiding;
    }
}
