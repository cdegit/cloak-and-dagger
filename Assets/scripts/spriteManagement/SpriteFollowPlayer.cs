using UnityEngine;
using System.Collections;

// TODO: Rename to player sprite manager or something
public class SpriteFollowPlayer : MonoBehaviour {
    private GameObject hunterSprite;
    private GameObject seekerSprite;

    private float hunterOffset;
    private float seekerOffset;

    private Renderer hunterRenderer;
    private Renderer seekerRenderer;

    private PlayerIdentity id;

	private bool inWater = false;

    void Start() {
        id = GetComponent<PlayerIdentity>();

        hunterSprite = GameObject.Find("Hunter Sprite");
        seekerSprite = GameObject.Find("Seeker Sprite");

        hunterRenderer = hunterSprite.GetComponent<Renderer>();
        seekerRenderer = seekerSprite.GetComponent<Renderer>();

        hunterOffset = hunterRenderer.bounds.size.y / 2;
        seekerOffset = seekerRenderer.bounds.size.y / 2;
    }
    

    void Update() {
        if (id.IsHunter()) {
			if (inWater) {
				// TODO: Lerp getting in and out of the water
				hunterSprite.transform.position = transform.position + new Vector3(0, hunterOffset/2, 0);
			} else {
				hunterSprite.transform.position = transform.position + new Vector3(0, hunterOffset, 0);
			}
        } else {
			if (inWater) {
				seekerSprite.transform.position = transform.position + new Vector3(0, seekerOffset/2, 0);
			} else {
				seekerSprite.transform.position = transform.position + new Vector3(0, seekerOffset, 0);
			}
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

	public void EnterWater() {
		inWater = true;
	}

	public void ExitWater() {
		inWater = false;
	}
}
