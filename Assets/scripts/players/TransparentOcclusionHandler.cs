using UnityEngine;
using System.Collections;

public class TransparentOcclusionHandler : MonoBehaviour {
    private float duration = 0.5f;
    private float t = 0f;

    private bool occludingPlayerThisFrame = false;

    private Renderer localRenderer;
    private Color originalColor;

    void Start() {
        localRenderer = GetComponent<Renderer>();
        originalColor = localRenderer.material.color;
    }

    void Update() {
        // Inspired by http://answers.unity3d.com/questions/328891/controlling-duration-of-colorlerp-in-seconds.html
        localRenderer.material.color = Color.Lerp(originalColor, new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f), t);

        if (occludingPlayerThisFrame) {
            // This will make the color more transparent over time
            if (t < 1) {
                t += Time.deltaTime / duration;
            }
        } else {
            // This will make the color more opaque over time
            if (t > 0) {
                t -= Time.deltaTime / duration;
            }
        }

        occludingPlayerThisFrame = false;
    }

    public void BecomeTranslucent() {
        occludingPlayerThisFrame = true;
    }
}
