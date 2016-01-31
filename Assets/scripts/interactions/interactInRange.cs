using UnityEngine;
using System.Collections;

// This script requires that the game object it is attached to has:
//     a Renderer
//     a 2D Collider set to trigger

public class interactInRange : MonoBehaviour {
    private bool withinRange = false;

    void Update() {
        if (withinRange) {
            // TODO: Check player type
            if (Input.GetButtonUp("Fire1")) {
                this.SeekerInteraction();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // highlight this object
        // show a GUI instruction (maybe)
        withinRange = true;
        GetComponent<Renderer>().material.color = Color.red;
    }

    void OnTriggerExit2D(Collider2D other) {
        withinRange = false;
        GetComponent<Renderer>().material.color = Color.white;
    }

    public virtual void SeekerInteraction() {

    }

    public virtual void HunterInteraction() {

    }
}
