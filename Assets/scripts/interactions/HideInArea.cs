using UnityEngine;
using System.Collections;

public class HideInArea : interactInRange {
    public override void SeekerInteraction(Collider thisPlayer) {
        HidingManager hidingManager = thisPlayer.gameObject.GetComponent<HidingManager>();

        if (!hidingManager.IsHiding()) {
            hidingManager.CmdHideInArea();
        } else {
            hidingManager.CmdStopHiding();
        }
    }

    public override void HunterInteraction(Collider thisPlayer) {
        // Na da
    }

	public override void OnTriggerExit(Collider thisPlayer) {
		base.OnTriggerExit(thisPlayer);
        // Leave grass when the player walks outside
        HidingManager hidingManager = thisPlayer.gameObject.GetComponent<HidingManager>();
        hidingManager.CmdStopHiding();
    }
}
