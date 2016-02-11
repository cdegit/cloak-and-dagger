using UnityEngine;
using System.Collections;

public class HunterAbilityUI : MonoBehaviour {
    public static HunterAbilityUI instance = null;

    public Texture2D emptyProgressBar;
    public Texture2D fullProgressBar;

    private float progress = 100;

    private PlayerIdentity id;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnGUI() {
        // Player manager doesn't have the players on start
        if (!id) {
            id = PlayerManager.instance.thisPlayer.GetComponent<PlayerIdentity>();
        }

        if (id.IsHunter()) {
            GUI.Label(new Rect(50, Screen.height - 70, 100, 20), "Echo - Right click");
            GUI.DrawTexture(new Rect(50, Screen.height - 50, 100, 10), emptyProgressBar);
            GUI.DrawTexture(new Rect(50, Screen.height - 50, progress, 10), fullProgressBar);
        }
    }

    public void UpdateProgress(float newProgress) {
        progress = Mathf.Min(newProgress, 100);
    }
}
