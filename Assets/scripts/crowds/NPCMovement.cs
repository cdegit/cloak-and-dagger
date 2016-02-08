using UnityEngine;
using System.Collections;

public class NPCMovement : MonoBehaviour {

    public Transform target;
    private NavMeshAgent navAgent;

	void Start () {
        navAgent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
	    if (target) {
            navAgent.SetDestination(target.transform.position);
        }
	}
}
