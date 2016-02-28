using UnityEngine;
using System.Collections;

// From http://answers.unity3d.com/answers/13187/view.html
public class disableShadows : MonoBehaviour {
	private float storedShadowDistance;

	void OnPreRender() {
		storedShadowDistance = QualitySettings.shadowDistance;
		QualitySettings.shadowDistance = 0;
	}

	void OnPostRender() {
		QualitySettings.shadowDistance = storedShadowDistance;
	}
}
