using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToTitle : MonoBehaviour {
	public void go() {
		SceneManager.LoadScene("titleScreen");
	}
}
