using UnityEngine;
using System.Collections;

public class HunterAbilityUI : MonoBehaviour {
    public static HunterAbilityUI instance = null;

    public Texture2D emptyProgressBar;
    public Texture2D fullProgressBar;

	public RenderTexture minimapTexture;
	public Material minimapMaterial;

	public float minimapDiameter = 200;

    private float progress = 100;

    private PlayerIdentity id;
	private PlayerMovement3D movement;

	private GUIStyle style;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

	void Start() {
		style = new GUIStyle();
		style.fontSize = 24;
		style.normal.textColor = Color.white;
	}

    void OnGUI() {
        // Player manager doesn't have the players on start
        if (!id) {
            if (PlayerManager.instance.thisPlayer) {
                id = PlayerManager.instance.thisPlayer.GetComponent<PlayerIdentity>();
				movement = PlayerManager.instance.thisPlayer.GetComponent<PlayerMovement3D>();
            }
        }

        if (id && id.IsHunter()) {
            GUI.Label(new Rect(50, Screen.height - 70, 100, 20), "Echo");
            GUI.DrawTexture(new Rect(50, Screen.height - 50, 100, 10), emptyProgressBar);
            GUI.DrawTexture(new Rect(50, Screen.height - 50, progress, 10), fullProgressBar);

			if (movement.hunterStartCountdown > 0) {
				GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 - 20, 100, 40), movement.hunterStartCountdown.ToString("F2"), style);
			}
        }

		// Graphics.DrawTexture should only be called at specific times
		// Using Graphics.DrawTexture instead of GUI.DrawTexture so we can use the material which gives us the mask
		// Referenced this tutorial for creating the minimap: https://youtu.be/ZuV9Xlt-l6g
		if (Event.current.type.Equals (EventType.Repaint)) {
			Graphics.DrawTexture (new Rect (Screen.width - (minimapDiameter + 25), 25, minimapDiameter, minimapDiameter), minimapTexture, minimapMaterial);
		}
    }

    public void UpdateProgress(float newProgress) {
        progress = Mathf.Min(newProgress, 100);
    }
}
