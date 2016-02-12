using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HidingManager : UnityEngine.Networking.NetworkBehaviour {
    private bool hiding = false;
    private bool stationary = false;

    private Renderer localRenderer;
    public GameObject currentHidingPlace;

    private bool emitParticlesWhenSeekerFound = false;

    void Start() {
        localRenderer = GetComponent<Renderer>();
    }

    [Command]
    public void CmdHide(GameObject hidingPlace) {
        currentHidingPlace = hidingPlace;
        RpcHide(hidingPlace);
    }

    [ClientRpc]
    private void RpcHide(GameObject hidingPlace) {
        // By making this an RPC, it gets executed on both clients
        // This will disable player movement
        hiding = true;
        stationary = true;

        // This makes the player invisible
        // Should also make it so the other player can't run into them
        localRenderer.enabled = false;

        currentHidingPlace = hidingPlace;
    }

    [Command]
    public void CmdStopHiding() {
        RpcStopHiding();
    }

    [ClientRpc]
    private void RpcStopHiding() {
        // Nudge the player slightly so we can see that they've left the hiding place
        if (stationary) {
            Vector3 nudgedPosition = transform.position + new Vector3(Random.Range(0f, 0.5f), 0, Random.Range(0f, 0.5f));
            transform.position = nudgedPosition;
        }

        // This is only set when the Hunter uses their echo ability at the moment
        if (emitParticlesWhenSeekerFound && currentHidingPlace) {
            ParticleEmitter emitter = currentHidingPlace.GetComponentInChildren<ParticleEmitter>();
            if (emitter) {
                emitter.Emit();
            }
        }

        hiding = false;
        stationary = false;
        currentHidingPlace = null;
        emitParticlesWhenSeekerFound = false;

        localRenderer.enabled = true;
    }

    public void CmdCheckHidingPlace(GameObject hidingPlace) {
        // Check if the Seeker is hiding in the same hiding place the Hunter is currently checking
        // If they are the same, kick the Seeker out of their hiding place
        if (!currentHidingPlace) {
            return;
        }

        if (currentHidingPlace.GetInstanceID() == hidingPlace.GetInstanceID()) {
            CmdStopHiding();
        }
    }

    public void EmitParticlesOnNextFind() {
        emitParticlesWhenSeekerFound = true;
    }

    public bool IsHiding() {
        return hiding;
    }

    public bool IsHidingStationary() {
        return hiding && stationary;
    }

    public bool IsHidingMovable() {
        return hiding && !stationary;
    }

    public Vector3 GetHidingPosition() {
        return currentHidingPlace.transform.position;
    }

    [Command]
    public void CmdHideInGrass(GameObject hidingPlace) {
        currentHidingPlace = hidingPlace;
        RpcHideInGrass(hidingPlace);
    }

    [ClientRpc]
    private void RpcHideInGrass(GameObject hidingPlace) {
        // Almost the same as hiding normally, but let the player move
        hiding = true;
        stationary = false;
        localRenderer.enabled = false;
        currentHidingPlace = hidingPlace;
    }
}
