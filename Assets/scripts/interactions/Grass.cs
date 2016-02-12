using UnityEngine;
using System.Collections;

public class Grass : interactInRange {
    private GameObject grassGameObject;

    void Start() {
        grassGameObject = gameObject;
    }

    public override void SeekerInteraction(Collider thisPlayer) {
        HidingManager hidingManager = thisPlayer.gameObject.GetComponent<HidingManager>();

        if (!hidingManager.IsHiding()) {
            hidingManager.CmdHideInGrass(grassGameObject);
        } else {
            hidingManager.CmdStopHiding();
        }
    }

    public override void HunterInteraction(Collider thisPlayer) {
        // Na da
    }

    void OnTriggerExit(Collider thisPlayer) {
        // Leave grass when the player walks outside
        HidingManager hidingManager = thisPlayer.gameObject.GetComponent<HidingManager>();
        hidingManager.CmdStopHiding();
    }
}
