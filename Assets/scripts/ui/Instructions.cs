using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {
	public bool autoPlay = false;

	Transform panel;
	int index = 0;

	void Start() {
		panel = transform.FindChild("ParentPanel");

		if (autoPlay) {
			StartCoroutine("AutoPlay");
		}
	}

	public void next() {
		if (index < panel.childCount - 1) {
			index++;
			showPanel();
		}
	}

	public void prev() {
		if (index > 0) {
			index--;
			showPanel();
		}
	}

	private void showPanel() {
		foreach (Transform child in panel) {
			child.gameObject.SetActive(false);
		}

		panel.GetChild(index).gameObject.SetActive(true);
	}

	IEnumerator AutoPlay() {
		yield return new WaitForSeconds(2);
		next();
		StartCoroutine("AutoPlay");
	}
}
