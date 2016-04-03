using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerMovement3D>().EnterWater();
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			other.gameObject.GetComponent<PlayerMovement3D>().ExitWater();
		}
	}
}
