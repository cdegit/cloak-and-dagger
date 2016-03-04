using UnityEngine;
using System.Collections;

public class Alley : interactInRange {
    // I recommend to put this game object and the otherEnd into the same parent object
    // To help keep the scene hierarchy less confusing
    public GameObject otherEnd;

    public override void SeekerInteraction(Collider thisPlayer) {
        // Teleport the seeker to the other end
        thisPlayer.gameObject.GetComponent<NavMeshAgent>().Warp(otherEnd.transform.position);

		// Lerp the camera position
		Camera.main.GetComponent<followTarget3D>().TargetWarped();
    }

    public override void HunterInteraction(Collider thisPlayer) {
        // Na da
    }
}
