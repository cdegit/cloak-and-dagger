using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HunterEcho : UnityEngine.Networking.NetworkBehaviour {
    private int layerIndex = 9;
    private int hidingPlaceLayerMask;

    private float echoSphereRadius = 5f;
    private float echoCooldownTime = 3f; // seconds
    private float echoCooldownTimer = Mathf.Infinity;

    private float echoMovementCooldownTime = 0.5f;
    private float echoMovementCooldownTimer = Mathf.Infinity;

    private ParticleEmitter echoParticleEmitter;

    private PlayerIdentity id;
    private HidingManager hidingManager;

	// Use this for initialization
	void Start () {
        hidingPlaceLayerMask = 1 << layerIndex;
        id = GetComponent<PlayerIdentity>();
        echoParticleEmitter = GetComponentInChildren<ParticleEmitter>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!id.IsHunter() || !id.IsThisPlayer()) {
            return;
        }

        // May not be available on start, so we'll do it here instead
        if (!hidingManager) {
            if (PlayerManager.instance.otherPlayer) {
                hidingManager = PlayerManager.instance.otherPlayer.GetComponent<HidingManager>();
            }
        }
        
	    if (echoCooldownTimer >= echoCooldownTime && Input.GetButtonUp("Fire2")) {
            UseEcho();
            echoCooldownTimer = 0;
            echoMovementCooldownTimer = 0;
        }
        
        if (echoCooldownTimer < echoCooldownTime) {
            echoCooldownTimer += Time.deltaTime;
        }

        // Don't let the player move for a short time after using this ability
        if (echoMovementCooldownTimer < echoMovementCooldownTime) {
            echoMovementCooldownTimer += Time.deltaTime;
        }

		UIManager.instance.UpdateProgress((echoCooldownTimer/echoCooldownTime) * 100);
    }

    // Send out a circle to some distance
    // Draw it as it goes along the ground ideally
    void UseEcho() {
        // First check if the player is even within their range
        float distanceToSeeker = Vector3.Distance(PlayerManager.instance.otherPlayer.transform.position, transform.position);

        if (distanceToSeeker <= echoSphereRadius) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, echoSphereRadius, hidingPlaceLayerMask);

            int i = 0;
            while (i < hitColliders.Length) {
                // Kick the Seeker out of their hiding place
                if (hidingManager) {
                    hidingManager.EmitParticlesOnNextFind();
                    hidingManager.CheckHidingPlace(hitColliders[i].gameObject);
                }
                i++;
            }
        }

		RpcRenderEchoEffect();
    }

    public bool CanHunterMove() {
        return echoMovementCooldownTimer >= echoMovementCooldownTime;
    }

	[ClientRpc]
	private void RpcRenderEchoEffect() {
		// Moved this into an RPC so the effect should be shown on both clients
		echoParticleEmitter.Emit();
	}
}
