using UnityEngine;
using System.Collections;

public class Instructions : MonoBehaviour {
	Transform panel;
	int index = 0;

	void Start() {
		panel = transform.FindChild("ParentPanel");
	}

	public void next() {
		if (index < 2) {
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
}
