using UnityEngine;
using System.Collections;

// Based on the code provided by Emily in the sample stealth project
public class NPCMovement : MonoBehaviour {
    public Transform[] waypoints;

    private NavMeshAgent navAgent;

    int waypointIndex = 0;

    float minDistance = 2f;
    float patrolWaitTime = 2;
    float patrolWaitTimer;

    void Start () {
        navAgent = GetComponent<NavMeshAgent>();

        // Disable rotation, so that the sprite is always facing the camera
        // ... Once the sprite is in place, of course
        navAgent.updateRotation = false;

        // Start moving to the first waypoint
		if (waypoints.Length > 0) {
        	navAgent.SetDestination(waypoints[waypointIndex].position);
		}
    }
	
	void Update () {
        if (ReferenceEquals(navAgent.destination, null) || navAgent.remainingDistance < minDistance) {
            //increment by Time.deltaTime because this increments wrt time (in seconds)
            patrolWaitTimer += Time.deltaTime;

            if (patrolWaitTimer >= patrolWaitTime) {
                if (waypointIndex >= waypoints.Length - 1) {
                    waypointIndex = 0;
                } else {
                    waypointIndex++;
                }
                
				if (waypoints.Length > 0) {
                	navAgent.SetDestination(waypoints[waypointIndex].position);
				}

                patrolWaitTimer = 0;
            }
        }
    }
}
