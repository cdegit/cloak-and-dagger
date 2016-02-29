using UnityEngine;
using System.Collections;

// This script requires that the game object it is attached to has:
//     a Collider set to trigger
// If it or a child has a sprite renderer, a yellow outline will appear around the object when the player is in range

public class interactInRange : MonoBehaviour {
	public Material outlineMaterial;
	private Material originalMaterial;

    protected GameObject otherPlayer;

	protected Renderer localRenderer;
	protected bool triedToFindRenderer = false;


    void Update() {
        if (!otherPlayer) {
            otherPlayer = PlayerManager.instance.otherPlayer;
        }

		if (!triedToFindRenderer) {
			localRenderer = GetComponentInChildren<SpriteRenderer>() as Renderer;

			// If there wasn't a sprite, there should be a mesh renderer to use
			if (!localRenderer) {
				localRenderer = GetComponent<Renderer>();
			}

			// Just in case we still haven't found one...
			if (localRenderer) {
				originalMaterial = localRenderer.material;
			}

			// If there just isn't one, don't keep looking
			triedToFindRenderer = true;
		}
    }

    void OnTriggerEnter(Collider other) {
        // Highlight this object
		if (localRenderer) {
			localRenderer.material = outlineMaterial;
		}
    }

    void OnTriggerExit(Collider other) {
		if (localRenderer) {
			localRenderer.material = originalMaterial;
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
