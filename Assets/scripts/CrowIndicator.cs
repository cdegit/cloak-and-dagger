using UnityEngine;
using System.Collections;

public class CrowIndicator : MonoBehaviour {
	private Transform target;
	private Renderer localRenderer;

	private float cooldownTime = 3f; // seconds
	private float cooldownTimer = Mathf.Infinity;

	void Start() {
		localRenderer = GetComponent<Renderer>();
		localRenderer.enabled = false;
	}

	void Update() {
		if (cooldownTimer < cooldownTime) {
			cooldownTimer += Time.deltaTime;
		}

		if (cooldownTimer > cooldownTime) {
			target = null;
			localRenderer.enabled = false;
		}
	}

	public void SetTarget(Transform crow) {
		target = crow;
		localRenderer.enabled = true;
		cooldownTimer = 0;

		Vector3 relative = transform.InverseTransformPoint(target.position);
		float angle = Mathf.Atan2(relative.x, relative.y)*Mathf.Rad2Deg;
		transform.Rotate(0,0, -angle);
	}
}
