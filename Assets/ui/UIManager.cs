using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	public static UIManager instance = null;

    public Texture2D emptyProgressBar;
    public Texture2D fullProgressBar;

	private float progress = 100;

	// Minimap variables
	public RenderTexture minimapTexture;
	public Material minimapMaterial;
	public float minimapDiameter = 200;
	public Texture2D podiumIndicator;
	private Rect minimapPosition;
	private Rect podiumIndicatorPosition;
	private Vector2 pivot;

	private GUIStyle style;

	private GameObject thisPlayer;
    private PlayerIdentity id;
	private PlayerMovement3D movement;
	private GameObject podium;

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

		podium = GameObject.Find("Podium");

		minimapPosition = new Rect(Screen.width - (minimapDiameter + 25), 25, minimapDiameter, minimapDiameter);
		podiumIndicatorPosition = new Rect(minimapPosition.center.x, minimapPosition.yMin, 16, 16);
		pivot = minimapPosition.center;
	}

    void OnGUI() {
        // Player manager doesn't have the players on start
        if (!id) {
            if (PlayerManager.instance.thisPlayer) {
				thisPlayer = PlayerManager.instance.thisPlayer;
                id = thisPlayer.GetComponent<PlayerIdentity>();
				movement = thisPlayer.GetComponent<PlayerMovement3D>();
            }
        }

		if (id && id.IsSeeker()) {
			DrawSeekerUI();
		}

        if (id && id.IsHunter()) {
			DrawHunterUI();
        }

		DrawMinimap();
    }

    public void UpdateProgress(float newProgress) {
        progress = Mathf.Min(newProgress, 100);
    }

	private void DrawSeekerUI() {
		GUI.Label(new Rect(50, Screen.height - 70, 100, 20), "Sprint");
		GUI.DrawTexture(new Rect(50, Screen.height - 50, 100, 10), emptyProgressBar);
		GUI.DrawTexture(new Rect(50, Screen.height - 50, progress, 10), fullProgressBar);
	}

	private void DrawHunterUI() {
		GUI.Label(new Rect(50, Screen.height - 70, 100, 20), "Echo");
		GUI.DrawTexture(new Rect(50, Screen.height - 50, 100, 10), emptyProgressBar);
		GUI.DrawTexture(new Rect(50, Screen.height - 50, progress, 10), fullProgressBar);

		if (movement.hunterStartCountdown > 0) {
			GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 - 20, 100, 40), movement.hunterStartCountdown.ToString("F2"), style);
		}
	}

	private void DrawMinimap() {
		// Graphics.DrawTexture should only be called at specific times
		// Using Graphics.DrawTexture instead of GUI.DrawTexture so we can use the material which gives us the mask
		// Referenced this tutorial for creating the minimap: https://youtu.be/ZuV9Xlt-l6g
		if (Event.current.type.Equals (EventType.Repaint)) {
			// Draw the minimap
			Graphics.DrawTexture(minimapPosition, minimapTexture, minimapMaterial);

			// Find the angle between the player and the podium
			if (!thisPlayer) {
				return;
			}

			Vector3 offset = podium.transform.position - thisPlayer.transform.position;
			float angle = Vector3.Angle(offset, thisPlayer.transform.right);

			if (offset.z > 0) {
				angle = 360 - angle;
			}

			// Rotate the GUI around the center of the minimap
			Matrix4x4 matrix = GUI.matrix;

			// Add 45 degree offset so that it matches the rotation of the minimap
			GUIUtility.RotateAroundPivot(angle + 45, pivot);

			// Draw the podium indicator
			GUI.DrawTexture(podiumIndicatorPosition, podiumIndicator);

			// Reset the GUI rotation
			GUI.matrix = matrix;
		}
	}
}
