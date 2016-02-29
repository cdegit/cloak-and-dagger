using UnityEngine;
using System.Collections;

public class TransparentOcclusion3D : MonoBehaviour {

	void FixedUpdate () {
        // Send a ray from the camera to the player
        // If the ray hits something that can occlude the player along the way, try to activate its Transparent Occlusion Handler
        // This will make the other object fade to 50% opacity
        RaycastHit hitInfo;
		RaycastHit[] hits;

		hits = Physics.RaycastAll(transform.position, (Camera.main.transform.position - transform.position), 100.0f);

		// TODO: Maybe cast from the ends of the sprite rather than the center, so the effect is a little cleaner
		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];
			TransparentOcclusionHandler occludingObjectHandler = hit.collider.gameObject.GetComponent<TransparentOcclusionHandler>();

			if (occludingObjectHandler) {
				occludingObjectHandler.BecomeTranslucent();
				occludingObjectHandler.SetSortingOrder(i + 1);
			}
		}
    }
}
