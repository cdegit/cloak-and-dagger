using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class basket : interactInRange {
    private NetworkInstanceId thisNetworkId;
    private GameObject otherPlayer;
    private GameObject basketGameObject;

    void Start() {
        basketGameObject = gameObject;
    }

    void FindOtherPlayer() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        
        foreach (GameObject go in gos) {
            if (go.GetComponent<PlayerIdentity>()) {
                if (!go.GetComponent<PlayerIdentity>().IsThisPlayer()) {
                    otherPlayer = go;
                }
            }
        }
    }

    public override void SeekerInteraction(Collider thisPlayer) {
        SeekerBehaviour seeker = thisPlayer.gameObject.GetComponent<SeekerBehaviour>();

        // Put the Seeker into hiding mode
        // Don't let them move and make them invisible
        if (!seeker.IsHiding()) {
            seeker.CmdHide(basketGameObject.transform.position);
        } else {
            seeker.CmdStopHiding();
        }
    }

    public override void HunterInteraction(Collider thisPlayer) {
        // If the Seeker is in the basket, remove them from hiding but don't immediately capture them
        
        if (!otherPlayer) {
            FindOtherPlayer();
        }

        // Call on the seeker object because it was easier thanks to networking issues....
        SeekerBehaviour seeker = otherPlayer.gameObject.GetComponent<SeekerBehaviour>();
        seeker.CmdCheckHidingPlace(basketGameObject.transform.position);
    }
}
