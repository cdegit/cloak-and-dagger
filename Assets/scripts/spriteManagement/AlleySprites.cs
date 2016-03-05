using UnityEngine;
using System.Collections;

public class AlleySprites : MonoBehaviour {
	private Animator animator;
	private GameObject otherEnd;

	void Start () {
		animator = GetComponent<Animator>();
		otherEnd = GetComponent<Alley>().otherEnd;

		Vector3 offset = otherEnd.transform.position - transform.position;

		float angle = Vector3.Angle(offset, transform.right);

		if (offset.z < 0) {
			angle = 360 - angle;
		}

		animator.SetFloat("angle", angle);
	}
}
