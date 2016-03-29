using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InWorldUI : MonoBehaviour {

	public bool onlyShowWhenChanging = false;

	private float progress = 100;
	private RectTransform rect;
	private Image img;
	private Color originalColor;

	void Start() {
		rect = GetComponent<RectTransform>();
		// img is the mask image
		img = transform.parent.gameObject.GetComponent<Image>();
		originalColor = img.color;
	}

	void Update() {
		if (!System.Single.IsInfinity(progress)) {
			rect.anchoredPosition = new Vector2(-1 + (progress / 100), 0);
		}

		if (onlyShowWhenChanging && (progress == 0 || progress == 100)) {
			img.color = new Color(0, 0, 0, 0);
		} else {
			img.color = originalColor;
		}
	}

	public void SetProgress(float p) {
		progress = p;
	}
}
