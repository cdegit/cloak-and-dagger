using UnityEngine;
using System.Collections;

// Set the correct icon depending on this player's type
public class minimapPlayerSprites : MonoBehaviour {
	public Sprite hunterSprite;
	public Sprite seekerSprite;

	void Start() {
		PlayerIdentity id = transform.parent.GetComponent<PlayerIdentity>();
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		if (id.IsHunter()) {
			spriteRenderer.sprite = hunterSprite;
		} else {
			spriteRenderer.sprite = seekerSprite;
		}
	}
}
