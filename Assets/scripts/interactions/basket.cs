using UnityEngine;
using System.Collections;

public class basket : interactInRange {

    public override void SeekerInteraction(Collider other) {
        Debug.Log("Seekers's interaction with basket");

        SeekerBehaviour seeker = other.gameObject.GetComponent<SeekerBehaviour>();

        // Put the Seeker into hiding mode
        // AKA Don't let them move around
        // Maybe first movement should let them exit the basket?
        // Or they should press a button to exit
        // Make the Seeker's renderer invisible
        // Disble interactions with the hunter's CAPTURE FIELD
        if (!seeker.IsHiding()) {
            seeker.CmdHide();
        } else {
            seeker.CmdStopHiding();
        }
    }

    public override void HunterInteraction(Collider other) {
        Debug.Log("Hunter's interaction with basket");
        // If the Seeker is in the basket, remove them from hiding but don't immediately capture them
    }
}
