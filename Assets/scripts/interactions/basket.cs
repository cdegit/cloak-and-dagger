using UnityEngine;
using System.Collections;

public class basket : interactInRange {

    public override void SeekerInteraction() {
        Debug.Log("Seekers's interaction with basket");
    }

    public override void HunterInteraction() {
        Debug.Log("Hunter's interaction with basket");
    }
}
