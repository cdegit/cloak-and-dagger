using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour {
    private Vector3 previousPosition;
    private float lastAngle;
    private Animator animator;
    
	void Start () {
        animator = GetComponent<Animator>();
        previousPosition = transform.position;
	}
	
	void Update () {
        Vector3 offset = transform.position - previousPosition;

        float angle = Vector3.Angle(offset, transform.right);
        float velocity = Vector3.Magnitude(offset);

        if (offset.z < 0) {
            angle = 360 - angle;
        }

        if (velocity == 0) {
            angle = lastAngle;
        }

        lastAngle = angle;
        previousPosition = transform.position;

        animator.SetFloat("velocity", velocity);
        animator.SetFloat("angle", angle);
    }
}
