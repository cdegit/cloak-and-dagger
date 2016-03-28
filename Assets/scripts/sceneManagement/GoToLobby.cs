using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToLobby : MonoBehaviour {
	public void go() {
		SceneManager.LoadScene("customLobby");
	}
}
