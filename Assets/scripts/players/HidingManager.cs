﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HidingManager : UnityEngine.Networking.NetworkBehaviour {
    private bool hiding = false;
    private bool stationary = false;

    private SpriteFollowPlayer playerSpriteManager;
    public GameObject currentHidingPlace;

    private bool emitParticlesWhenSeekerFound = false;

    void Start() {
        // We actually need the renderer for the sprite
        // Actually get a reference to SpriteFollowPlayer and make that do the work
        playerSpriteManager = GetComponent<SpriteFollowPlayer>();
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
        playerSpriteManager.MakeSpriteInvisible();

        currentHidingPlace = hidingPlace;
    }

    [Command]
    public void CmdStopHiding() {
        RpcStopHiding();
    }

    [ClientRpc]
    private void RpcStopHiding() {
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

        playerSpriteManager.MakeSpriteVisible();
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
        playerSpriteManager.MakeSpriteInvisible();
        currentHidingPlace = hidingPlace;
    }
}
