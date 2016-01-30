using UnityEngine;
using System.Collections;

// Based on the example code from the SamplePlatformerDryRun
public class followTarget : MonoBehaviour {
    public Transform target = null;

    public float dragX = 1f;
    public float dragY = 1f;

	void FixedUpdate () {
        // GROSS DEPENDENCY: For the camera, target is set by the PlayerMovement script
        // This is because the player is a prefab and we can't access it directly
        if (target != null) {
            transform.position = new Vector3(target.position.x * dragX, target.position.y * dragX, transform.position.z);
        }
	}
}
