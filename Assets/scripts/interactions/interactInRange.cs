using UnityEngine;
using System.Collections;

// This script requires that the game object it is attached to has:
//     a Collider set to trigger
// If it or a child has a sprite renderer, a yellow outline will appear around the object when the player is in range

public class interactInRange : MonoBehaviour {
	public Material outlineMaterial;
	private Material originalMaterial;

    protected GameObject otherPlayer;

	protected SpriteRenderer localSpriteRenderer;
	protected bool triedToFindRenderer = false;


    void Update() {
        if (!otherPlayer) {
            otherPlayer = PlayerManager.instance.otherPlayer;
        }

		if (!triedToFindRenderer) {
			localSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
			triedToFindRenderer = true;

			if (localSpriteRenderer) {
				originalMaterial = localSpriteRenderer.material;
			}
		}
    }

    void OnTriggerEnter(Collider other) {
        // Highlight this object
		if (localSpriteRenderer) {
			localSpriteRenderer.material = outlineMaterial;
		}
    }

    void OnTriggerExit(Collider other) {
		if (localSpriteRenderer) {
			localSpriteRenderer.material = originalMaterial;
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
