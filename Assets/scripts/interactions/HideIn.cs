using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class HideIn : interactInRange {
    private GameObject basketGameObject;

    void Start() {
        basketGameObject = gameObject;
    }

    public override void SeekerInteraction(Collider thisPlayer) {
        HidingManager hidingManager = thisPlayer.gameObject.GetComponent<HidingManager>();

        // Put the Seeker into hiding mode
        // Don't let them move and make them invisible
        if (!hidingManager.IsHiding()) {
            hidingManager.CmdHideInObject(basketGameObject);
        } else {
            hidingManager.CmdStopHiding();
        }
    }

    public override void HunterInteraction(Collider thisPlayer) {
        // If the Seeker is in this hiding place, remove them from hiding but don't immediately capture them
        // Call on the seeker object because it was easier thanks to networking issues....
        HidingManager hidingManager = otherPlayer.gameObject.GetComponent<HidingManager>();
        hidingManager.CheckHidingPlace(basketGameObject);
    }
}
