using UnityEngine;
using System.Collections;

public class ZBuffer3D : MonoBehaviour {

    public bool canMove = false;

    // gives a warning if called renderer
    // but can't access renderer directly as its deprecated :(
    // so a new name is needed
    private Renderer localRenderer;
    private float halfSpriteHeight;
    private int maxSortingOrderValue = 1000;

    // Inspired by: http://forum.unity3d.com/threads/2d-isometric-in-unity-2d-mode.221134/#post-1475648
    void Start() {
        localRenderer = GetComponent<Renderer>();
        halfSpriteHeight = GetComponent<SpriteRenderer>().bounds.extents.y;

        setSortingOrder();
    }

    void FixedUpdate() {
        if (canMove) {
            setSortingOrder();
        }
    }

    void setSortingOrder() {
        // Up is +z, down is -z
        float bottomZ = transform.position.z - halfSpriteHeight;

        // Position can become negative due to where the ground is placed
        // Shift out of negative values for easier math
        bottomZ += 10;

        // Scale by 10 so we can distinguish between small differences. Otherwise they'd be .1 or something.
        bottomZ *= 10;

        localRenderer.sortingOrder = maxSortingOrderValue - (int)Mathf.Floor(bottomZ);
        //Vector3 newPosition = transform.position;
        //newPosition.y = maxDepth - (int)Mathf.Floor(bottomZ);
        //transform.position = newPosition;
    }
}
