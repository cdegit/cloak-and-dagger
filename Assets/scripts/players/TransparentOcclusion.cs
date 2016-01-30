using UnityEngine;
using System.Collections;

public class TransparentOcclusion : MonoBehaviour {
    private Color lerpedColor = Color.white;
    private float duration = 0.5f;
    private float t = 0f;

    private bool collisionOccuring = false;

    void Update()
    {
         GetComponent<Renderer>().material.color = Color.Lerp(Color.white, new Color(1.0f, 1.0f, 1.0f, 0.5f), t);

        if (collisionOccuring) {
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
    }

    void OnTriggerEnter2D(Collider2D other) {
        // reset control variable t so the lerp can restart
        t = 0f;
        collisionOccuring = true;
    }

    void OnTriggerExit2D(Collider2D other) {
        // reset control variable t so the lerp can restart
        t = duration;
        collisionOccuring = false;
    }
}
