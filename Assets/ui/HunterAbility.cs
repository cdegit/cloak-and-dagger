using UnityEngine;
using System.Collections;

public class HunterAbility : MonoBehaviour {
    public Texture2D emptyProgressBar;
    public Texture2D fullProgressBar;

    void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, 100, 50), emptyProgressBar);
        GUI.DrawTexture(new Rect(0, 0, 50, 50), fullProgressBar);
    }
}
