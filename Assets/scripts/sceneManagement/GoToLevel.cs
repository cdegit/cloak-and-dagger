using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour {
	public void go() {
		SceneManager.LoadScene("levelABC");
	}
}
