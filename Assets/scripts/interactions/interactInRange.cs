using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

// This script requires that the game object it is attached to has:
//     a Collider set to trigger
// If it or a child has a sprite renderer, a yellow outline will appear around the object when the player is in range

public class interactInRange : NetworkBehaviour {
	public Material outlineMaterial;
	private Material originalMaterial;

    protected GameObject otherPlayer;

	public Renderer localRenderer;
	protected bool triedToFindRenderer = false;


	public virtual void Update() {
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

    public virtual void OnTriggerEnter(Collider other) {
		ShowOutline();
    }

    public virtual void OnTriggerExit(Collider other) {
		HideOutline();
    }

    public virtual void OnTriggerStay (Collider other) {
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

	public void ShowOutline() {
		// Highlight this object
		if (localRenderer && outlineMaterial) {
			localRenderer.material = outlineMaterial;
		}
	}

	public void HideOutline() {
		if (localRenderer) {
			localRenderer.material = originalMaterial;
		}
	}
}
