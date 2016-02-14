﻿using UnityEngine;
using System.Collections;

public class SpriteFollowPlayer : MonoBehaviour {
    
    private GameObject hunterSprite;
    private GameObject seekerSprite;

    private float hunterOffset;
    private float seekerOffset;

    private PlayerIdentity id;

    void Start() {
        id = GetComponent<PlayerIdentity>();

        hunterSprite = GameObject.Find("Hunter Sprite");
        seekerSprite = GameObject.Find("Seeker Sprite");

        hunterOffset = hunterSprite.GetComponent<Renderer>().bounds.size.y / 2;
        seekerOffset = seekerSprite.GetComponent<Renderer>().bounds.size.y / 2;
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
            hunterSprite.GetComponent<Animator>().SetFloat("velocity", velocity);
            hunterSprite.GetComponent<Animator>().SetFloat("angle", angle);
        } else {
            // Seeker stuff here
            // TODO: This won't animate the non local player
        }
    }
}
