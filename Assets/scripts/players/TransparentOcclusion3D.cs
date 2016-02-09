using UnityEngine;
using System.Collections;

public class TransparentOcclusion3D : MonoBehaviour {

	void FixedUpdate () {
        // Send a ray from the camera to the player
        // If the ray hits something that can occlude the player along the way, try to activate its Transparent Occlusion Handler
        // This will make the other object fade to 50% opacity
        RaycastHit hitInfo;

        if (Physics.Linecast(Camera.main.transform.position, transform.position, out hitInfo)) {
            // Currently the object needs to have both the tag and the script
            // The script alone may be sufficient
            if (hitInfo.collider.CompareTag("Occludable")) {
                TransparentOcclusionHandler occludingObjectHandler = hitInfo.collider.gameObject.GetComponent<TransparentOcclusionHandler>();

                if (occludingObjectHandler) {
                    occludingObjectHandler.BecomeTranslucent();
                }
            }
        }
    }
}
