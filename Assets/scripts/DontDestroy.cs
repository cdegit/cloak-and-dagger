using UnityEngine;
using System.Collections;

// http://answers.unity3d.com/questions/11314/audio-or-music-to-continue-playing-between-scene-c.html
public class DontDestroy : MonoBehaviour {
	private static DontDestroy instance = null;

	public static DontDestroy Instance {
		get {
			return instance;
		}
	}

	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
