using UnityEngine;
using System.Collections;

public class PlayerIdentity : UnityEngine.Networking.NetworkBehaviour {
    private bool isHunter;

    void Start () {
        // Until we allow the players to select their character:
        // If this is the host, set the localPlayer to the Hunter and the other player to the Seeker
        // If this is the client, set the localPlayer to the Seeker and the other player to the Hunter
        // TL;DR If you host, you play as the Hunter

        // TODO: Object interactions break if this is !isServer
        // Look into that...
        if (isServer) {
            if (isLocalPlayer) {
                InitAsHunter();
            } else {
                InitAsSeeker();
            }
        } else {
            if (isLocalPlayer) {
                InitAsSeeker();
            } else {
                InitAsHunter();
            }
        }
    }

    void InitAsHunter () {
		GameObject spawn = GameObject.Find("Hunter Spawn Point");

		if (spawn) {
			GetComponent<NavMeshAgent>().Warp(spawn.transform.position);
		}

		isHunter = true;
    }

    void InitAsSeeker () {
		GameObject spawn = GameObject.Find("Seeker Spawn Point");

		if (spawn) {
			GetComponent<NavMeshAgent>().Warp(spawn.transform.position);
		}

        isHunter = false;
    }

    public bool IsHunter () {
        return isHunter;
    }

    public bool IsSeeker () {
        return !isHunter;
    }

    public bool IsThisPlayer() {
        return isLocalPlayer;
    }
}
