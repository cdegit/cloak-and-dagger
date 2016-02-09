using UnityEngine;
using System.Collections;

// Based on the example code from the SamplePlatformerDryRun
public class followTarget3D : MonoBehaviour {
    public Transform target = null;

    private float offset = 3f;

    void FixedUpdate() {
        // GROSS DEPENDENCY: For the camera, target is set by the PlayerMovement script
        // This is because the player is a prefab and we can't access it directly
        if (target != null) {
            // Offset roughly centers the camera
            // TODO: Maybe get the actual size of the screen so this offset will work for all resolutions
            transform.position = new Vector3(target.position.x - offset, transform.position.y, target.position.z - offset);
        }
    }
}
