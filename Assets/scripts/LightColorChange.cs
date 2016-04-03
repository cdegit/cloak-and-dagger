using UnityEngine;
using System.Collections;

public class LightColorChange : MonoBehaviour {
	private Light lt;
	public Color startColor;
	public Color endColor;
	public float duration;

	void Start () {
		lt = GetComponent<Light>();
		lt.color = startColor;
	}

	void Update () {
		lt.color = Color.Lerp(startColor, endColor, Time.time / duration);
	}
}
