using UnityEngine;
using System.Collections;

public class EscToQuit : MonoBehaviour {
	void Update() {
		if (Input.GetKey("escape")) {
			Application.Quit();
		}
	}
}
