using UnityEngine;
using System.Collections;

public class TransparentOcclusion3D : MonoBehaviour {
	private PlayerIdentity id;
	private Renderer spriteRenderer;

	void Start() {
		id = GetComponent<PlayerIdentity>();

		if (id.IsHunter()) {
			spriteRenderer = GameObject.Find("Hunter Sprite").GetComponent<Renderer>();
		} else {
			spriteRenderer = GameObject.Find("Seeker Sprite").GetComponent<Renderer>();
		}
	}

	void FixedUpdate() {
        // Send a ray from the camera to the player
        // If the ray hits something that can occlude the player along the way, try to activate its Transparent Occlusion Handler
        // This will make the other object fade to 50% opacity
		RaycastHit[] leftHits;
		RaycastHit[] middleHits;
		RaycastHit[] rightHits;

		float x = spriteRenderer.transform.position.x;
		float y = spriteRenderer.transform.position.y;
		float z = spriteRenderer.transform.position.z;
		float extentsX = spriteRenderer.bounds.extents.x;
		float extentsY = spriteRenderer.bounds.extents.y;

		Vector3 leftRay = new Vector3(x - extentsX, y - extentsY, z);
		Vector3 middleRay = new Vector3(x, y - extentsY, z);
		Vector3 rightRay = new Vector3(x + extentsX, y - extentsY, z);

		leftHits = Physics.RaycastAll(leftRay, (Camera.main.transform.position - leftRay), 100.0f);
		middleHits = Physics.RaycastAll(middleRay, (Camera.main.transform.position - middleRay), 100.0f);
		rightHits = Physics.RaycastAll(rightRay, (Camera.main.transform.position - rightRay), 100.0f);

		HandleHits(leftHits);
		HandleHits(middleHits);
		HandleHits(rightHits);
    }

	void HandleHits(RaycastHit[] hits) {
		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];
			TransparentOcclusionHandler occludingObjectHandler = hit.collider.gameObject.GetComponent<TransparentOcclusionHandler>();

			if (occludingObjectHandler) {
				occludingObjectHandler.BecomeTranslucent();
				occludingObjectHandler.SetSortingOrder(i + 2);
			}
		}
	}
}
