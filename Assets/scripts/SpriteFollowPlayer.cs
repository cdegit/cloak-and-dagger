using UnityEngine;
using System.Collections;

// TODO: Rename to player sprite manager or something
public class SpriteFollowPlayer : MonoBehaviour {
    
    private GameObject hunterSprite;
    private GameObject seekerSprite;

    private float hunterOffset;
    private float seekerOffset;

    private Animator hunterAnimator;
    private Animator seekerAnimator;

    private Renderer hunterRenderer;
    private Renderer seekerRenderer;

    private PlayerIdentity id;

    void Start() {
        id = GetComponent<PlayerIdentity>();

        hunterSprite = GameObject.Find("Hunter Sprite");
        seekerSprite = GameObject.Find("Seeker Sprite");

        hunterRenderer = hunterSprite.GetComponent<Renderer>();
        seekerRenderer = seekerSprite.GetComponent<Renderer>();

        hunterOffset = hunterRenderer.bounds.size.y / 2;
        seekerOffset = seekerRenderer.bounds.size.y / 2;

        hunterAnimator = hunterSprite.GetComponent<Animator>();
        seekerAnimator = seekerSprite.GetComponent<Animator>();
    }
    

    void Update() {
        if (id.IsHunter()) {
            hunterSprite.transform.position = transform.position + new Vector3(0, hunterOffset, 0);
        } else {
            seekerSprite.transform.position = transform.position + new Vector3(0, seekerOffset, 0);
        }
	}

    public void SetAnimationParams(float velocity, float angle) {
        if (id.IsHunter()) {
            hunterAnimator.SetFloat("velocity", velocity);
            hunterAnimator.SetFloat("angle", angle);
        } else {
            // Seeker stuff here
            // TODO: This won't animate the non local player
        }
    }

    public void MakeSpriteInvisible() {
        if (id.IsHunter()) {
            hunterRenderer.enabled = false;
        } else {
            seekerRenderer.enabled = false;
        }
    }

    public void MakeSpriteVisible() {
        if (id.IsHunter()) {
            hunterRenderer.enabled = true;
        } else {
            seekerRenderer.enabled = true;
        }
    }
}
