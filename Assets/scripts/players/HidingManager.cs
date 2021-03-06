﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HidingManager : UnityEngine.Networking.NetworkBehaviour {
	public GameObject currentHidingPlace;

    private bool hiding = false;
	private bool hidingInArea = false;

    private SpriteFollowPlayer playerSpriteManager;
	private Renderer minimapSpriteRenderer;
	private ParticleEmitter seekerParticleEmitter;

    private bool emitParticlesWhenSeekerFound = false;

    void Start() {
        // We actually need the renderer for the sprite
        // Actually get a reference to SpriteFollowPlayer and make that do the work
        playerSpriteManager = GetComponent<SpriteFollowPlayer>();
		minimapSpriteRenderer = transform.Find("Minimap Icon").GetComponent<Renderer>();
		seekerParticleEmitter = transform.Find("Echo'd Particle Emitter").GetComponent<ParticleEmitter>();
    }

	[Command]
	public void CmdHideInObject(GameObject hidingPlace) {
		// stores a reference to the object and uses that
		RpcHideInObject(hidingPlace);
	}

	[Command]
	public void CmdHideInArea() {
		// doesn't store a reference to the object - maybe stores location instead?
		// grass and water shouldn't move
		RpcHideInArea();
	}

	[ClientRpc]
	private void RpcHideInObject(GameObject hidingPlace) {
		Animator anim = hidingPlace.GetComponentInChildren<Animator>();
		if (anim) {
			anim.Play("get in");
		}

		hidingPlace.GetComponent<interactInRange>().HideOutline();

		currentHidingPlace = hidingPlace;
		hide();
	}

	[ClientRpc]
	private void RpcHideInArea() {
		hidingInArea = true;
		hide();
	}

	private void hide() {
		// This makes the player invisible
		// Should also make it so the other player can't run into them
		playerSpriteManager.MakeSpriteInvisible();
		minimapSpriteRenderer.enabled = false;
		hiding = true;
	}

	public void CheckHidingPlace(GameObject hidingPlace) {
		// If they're hiding in an area, this is only called if the hunter is close enough
		// Should the hunter be responsible for that?
		if (hidingInArea) {
			CmdStopHiding();
		}

		if (!currentHidingPlace) {
			return;
		}

		// Check if the Seeker is hiding in the same hiding place the Hunter is currently checking
		// If they are the same, kick the Seeker out of their hiding place
		if (currentHidingPlace.GetInstanceID() == hidingPlace.GetInstanceID()) {
			CmdStopHiding();
		}
	}

    [Command]
    public void CmdStopHiding() {
        RpcStopHiding();
    }

    [ClientRpc]
    private void RpcStopHiding() {
        // This is only set when the Hunter uses their echo ability at the moment
        if (emitParticlesWhenSeekerFound) {
			if (seekerParticleEmitter) {
				seekerParticleEmitter.Emit();
            }
        }

		if (currentHidingPlace) {
			Transform basketSprite = currentHidingPlace.transform.Find("Basket Sprite");
			if (basketSprite) {
				Animator anim = basketSprite.gameObject.GetComponent<Animator>();
				if (anim) {
					// If we're playing the animation, don't show the player or let them move right away
					anim.Play("get out");
					return;
				}
			}
			currentHidingPlace.GetComponent<interactInRange>().ShowOutline();
		}

		CmdFinishedAnimation();
    }

	[Command]
	public void CmdFinishedAnimation() {
		RpcResetHidingVars();
	}

	[ClientRpc]
	private void RpcResetHidingVars() {
		hiding = false;
		hidingInArea = false;
		currentHidingPlace = null;
		emitParticlesWhenSeekerFound = false;

		playerSpriteManager.MakeSpriteVisible();
		minimapSpriteRenderer.enabled = true;
	}

    public void EmitParticlesOnNextFind() {
        emitParticlesWhenSeekerFound = true;
    }

    public bool IsHiding() {
        return hiding;
    }

    public bool IsHidingStationary() {
		return hiding && !hidingInArea;
    }

    public bool IsHidingMovable() {
		return hiding && hidingInArea;
    }
}
