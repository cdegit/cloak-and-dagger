using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToInstructions : MonoBehaviour {
	public void go() {
		SceneManager.LoadScene("instructions");
	}
}
