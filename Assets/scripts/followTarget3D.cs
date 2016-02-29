using UnityEngine;
using System.Collections;

// Based on the example code from the SamplePlatformerDryRun
public class followTarget3D : MonoBehaviour {
    public Transform target = null;

	// If this is the main camera, use an offset
	// If this is the minimap, use none
    private float offset = 0f;

	private bool targetJustWarped = false;
	private float speed = 7.5f;
	private float startTime;
	private float journeyLength;
	private Vector3 lerpStartPosition;

	void Start() {
		if (gameObject.tag == "MainCamera") {
			offset = 11f;
		}
	}

    void FixedUpdate() {
        // GROSS DEPENDENCY: For the camera, target is set by the PlayerMovement script
        // This is because the player is a prefab and we can't access it directly
        if (target != null) {
			if (targetJustWarped) {
				// If the target has just used an alley, we want to smoothly move the camera to the new position so it isn't super jarring
				float distanceCovered = (Time.time - startTime) * speed;
				float fracJourney = distanceCovered / journeyLength;

				transform.position = Vector3.Lerp(lerpStartPosition, GetNewPosition(), fracJourney);

				// If we've caught up with the player, stop the smooth movement
				if (fracJourney == 1) {
					targetJustWarped = false;
				}
			} else {
				// For the main camera, offset roughly centers the camera
				transform.position = GetNewPosition();
			}
        }
    }

	public void TargetWarped() {
		targetJustWarped = true;

		lerpStartPosition = transform.position;
		startTime = Time.time;
		journeyLength = Vector3.Distance(lerpStartPosition, GetNewPosition());
	}

	private Vector3 GetNewPosition() {
		return new Vector3(target.position.x - offset, transform.position.y, target.position.z - offset);
	}
}
