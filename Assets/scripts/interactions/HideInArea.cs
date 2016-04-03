using UnityEngine;
using System.Collections;

public class HideInArea : interactInRange {
    public override void SeekerInteraction(Collider thisPlayer) {
        HidingManager hidingManager = thisPlayer.gameObject.GetComponent<HidingManager>();
		PlayerMovement3D movement = thisPlayer.gameObject.GetComponent<PlayerMovement3D>();

        if (!hidingManager.IsHiding()) {
            hidingManager.CmdHideInArea();
			movement.onGrass = false;
			movement.inGrass = true;
        } else {
            hidingManager.CmdStopHiding();
			movement.onGrass = true;
			movement.inGrass = false;
        }
    }

    public override void HunterInteraction(Collider thisPlayer) {
        // Na da
    }

	public override void OnTriggerExit(Collider thisPlayer) {
		base.OnTriggerExit(thisPlayer);
        // Leave grass when the player walks outside
        HidingManager hidingManager = thisPlayer.gameObject.GetComponent<HidingManager>();
		PlayerMovement3D movement = thisPlayer.gameObject.GetComponent<PlayerMovement3D>();

        hidingManager.CmdStopHiding();

		movement.inGrass = false;
		movement.onGrass = false;
    }
}
