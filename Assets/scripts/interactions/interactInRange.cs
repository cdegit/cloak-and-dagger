﻿using UnityEngine;
using System.Collections;

// This script requires that the game object it is attached to has:
//     a Renderer
//     a Collider set to trigger

public class interactInRange : MonoBehaviour {
    private Renderer localRenderer;
    protected GameObject otherPlayer;

    void Start() {
        localRenderer = GetComponent<Renderer>();
    }

    void Update() {
        if (!otherPlayer) {
            otherPlayer = PlayerManager.instance.otherPlayer;
        }
    }

    void OnTriggerEnter(Collider other) {
        // highlight this object
        // show a GUI instruction (maybe)
        if (localRenderer) {
            localRenderer.material.color = Color.red;
        }
    }

    void OnTriggerExit(Collider other) {
        if (localRenderer) {
            localRenderer.material.color = Color.white;
        }
    }

    // Note: For this to work, the rigidbody in the player must be set to never sleep
    // Otherwise this just isn't fired
    void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Player" && Input.GetButtonUp("Fire1")) {
            PlayerIdentity id = other.gameObject.GetComponent<PlayerIdentity>();
            
            
            // Need to make sure that the input only affects the appropriate player
            if (id.IsThisPlayer()) {
                if (id.IsHunter()) {
                    this.HunterInteraction(other);
                } else {
                    this.SeekerInteraction(other);
                }
            }
        }
    }

    public virtual void SeekerInteraction(Collider other) {

    }

    public virtual void HunterInteraction(Collider other) {

    }
}