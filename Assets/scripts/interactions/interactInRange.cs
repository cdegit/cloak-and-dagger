using UnityEngine;
using System.Collections;

// This script requires that the game object it is attached to has:
//     a Renderer
//     a Collider set to trigger

public class interactInRange : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        // highlight this object
        // show a GUI instruction (maybe)
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnTriggerExit(Collider other) {
        GetComponent<Renderer>().material.color = Color.white;
    }

    // Note: For this to work, the rigidbody in the player must be set to never sleep
    // Otherwise this just isn't fired

    // TODO: If you push the other player into an item and click, it'll register as an interaction for them
    // Player identity should tell you if you're the current player (so not everything needs to extend network behaviour)
    void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Player" && Input.GetButtonUp("Fire1")) {
            if (other.gameObject.GetComponent<PlayerIdentity>().IsHunter()) {
                this.HunterInteraction();
            } else {
                this.SeekerInteraction();
            }
        }
    }

    public virtual void SeekerInteraction() {

    }

    public virtual void HunterInteraction() {

    }
}
