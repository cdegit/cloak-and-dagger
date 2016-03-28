using UnityEngine;
using System.Collections;

public class InWorldUI : MonoBehaviour {
	private float progress = 100;
	private RectTransform rect;

	void Start() {
		rect = GetComponent<RectTransform>();
	}

	void Update() {
		if (!System.Single.IsInfinity(progress)) {
			rect.anchoredPosition = new Vector2(-1 + (progress / 100), 0);
		}
	}

	public void SetProgress(float p) {
		progress = p;
	}
}
