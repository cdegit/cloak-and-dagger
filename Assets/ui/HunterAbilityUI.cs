using UnityEngine;
using System.Collections;

public class HunterAbilityUI : MonoBehaviour {
    public static HunterAbilityUI instance = null;

    public Texture2D emptyProgressBar;
    public Texture2D fullProgressBar;

	public RenderTexture minimapTexture;
	public Material minimapMaterial;

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
            if (PlayerManager.instance.thisPlayer) {
                id = PlayerManager.instance.thisPlayer.GetComponent<PlayerIdentity>();
            }
        }

        if (id && id.IsHunter()) {
            GUI.Label(new Rect(50, Screen.height - 70, 100, 20), "Echo - Right click");
            GUI.DrawTexture(new Rect(50, Screen.height - 50, 100, 10), emptyProgressBar);
            GUI.DrawTexture(new Rect(50, Screen.height - 50, progress, 10), fullProgressBar);
        }

		// Graphics.DrawTexture should only be called at specific times
		// Using Graphics.DrawTexture instead of GUI.DrawTexture so we can use the material which gives us the mask
		// Referenced this tutorial for creating the minimap: https://youtu.be/ZuV9Xlt-l6g
		if (Event.current.type.Equals (EventType.Repaint)) {
			Graphics.DrawTexture (new Rect (Screen.width - 175, 25, 150, 150), minimapTexture, minimapMaterial);
		}
    }

    public void UpdateProgress(float newProgress) {
        progress = Mathf.Min(newProgress, 100);
    }
}
