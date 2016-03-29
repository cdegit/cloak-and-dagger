using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour {
	public void GoToTitle() {
		SceneManager.LoadScene("titleScreen");
	}

	public void GoToInstructions() {
		SceneManager.LoadScene("instructions");
	}

	public void GoToCutscene() {
		SceneManager.LoadScene("cutscene");
	}

	public void GoToLobby() {
		SceneManager.LoadScene("customLobby");
	}

	public void GoToLevel() {
		SceneManager.LoadScene("microLevelA");
	}
}
