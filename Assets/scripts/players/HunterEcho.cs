using UnityEngine;
using System.Collections;

public class HunterEcho : MonoBehaviour {
    private int layerIndex = 9;
    private int hidingPlaceLayerMask;

    private float echoSphereRadius = 5f;
    private float echoCooldownTime = 3f; // seconds
    private float echoCooldownTimer = Mathf.Infinity;

    private bool isHunter;

    private HidingManager hidingManager;

	// Use this for initialization
	void Start () {
        hidingPlaceLayerMask = 1 << layerIndex;
        isHunter = GetComponent<PlayerIdentity>().IsHunter();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isHunter) {
            return;
        }

        // May not be available on start, so we'll do it here instead
        if (!hidingManager) {
            if (PlayerManager.instance.otherPlayer) {
                hidingManager = PlayerManager.instance.otherPlayer.GetComponent<HidingManager>();
            }
        }

        // TODO:
        // It should probably be a different button
        // Also also will need to show cooldown on GUI or on player somehow
        // Do it with text for now
	    if (echoCooldownTimer > echoCooldownTime && Input.GetButtonUp("Fire1")) {
            UseEcho();
            echoCooldownTimer = 0;
        }
        
        if (echoCooldownTimer < echoCooldownTime) {
            echoCooldownTimer += Time.deltaTime;
        }
    }

    // Send out a circle to some distance
    // Draw it as it goes along the ground ideally
    void UseEcho() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, echoSphereRadius, hidingPlaceLayerMask);

        int i = 0;
        while (i < hitColliders.Length) {
            // Kick the Seeker out of their hiding place
            hidingManager.CmdCheckHidingPlace(hitColliders[i].gameObject);
            i++;
        }
    }
}
