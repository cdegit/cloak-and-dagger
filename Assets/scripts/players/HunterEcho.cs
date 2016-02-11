using UnityEngine;
using System.Collections;

public class HunterEcho : MonoBehaviour {
    private int layerIndex = 9;
    private int hidingPlaceLayerMask;

    private float echoSphereRadius = 5f;
    private float echoCooldownTime = 3f; // seconds
    private float echoCooldownTimer = Mathf.Infinity;

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

        // TODO: Should lock the hunter in place for a short time as well
	    if (echoCooldownTimer > echoCooldownTime && Input.GetButtonUp("Fire2")) {
            UseEcho();
            echoCooldownTimer = 0;
        }
        
        if (echoCooldownTimer < echoCooldownTime) {
            echoCooldownTimer += Time.deltaTime;
        }

        HunterAbility.instance.UpdateProgress((echoCooldownTimer/echoCooldownTime) * 100);
    }

    // Send out a circle to some distance
    // Draw it as it goes along the ground ideally
    void UseEcho() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, echoSphereRadius, hidingPlaceLayerMask);

        int i = 0;
        while (i < hitColliders.Length) {
            // Kick the Seeker out of their hiding place
            if (hidingManager) {
                hidingManager.EmitParticlesOnNextFind();
                hidingManager.CmdCheckHidingPlace(hitColliders[i].gameObject);
            }
            i++;
        }

        echoParticleEmitter.Emit();
    }
}
