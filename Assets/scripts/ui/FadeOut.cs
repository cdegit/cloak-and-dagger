using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour {
	private Image img;

	void Start () {
		img = GetComponent<Image>();
	}

	void Update () {
		img.color = Color.Lerp(Color.black, Color.clear, Time.timeSinceLevelLoad / 2f);
	}
}
