using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GatePlayer : NetworkBehaviour {
	public void UnlockGate(GameObject gate) {
		CmdUnlockGate(gate);
	}

	[Command]
	private void CmdUnlockGate(GameObject gate) {
		RpcUnlockGate(gate);
	}

	[ClientRpc]
	private void RpcUnlockGate(GameObject gate) {
		gate.transform.FindChild("desert gate").gameObject.SetActive(false);
		gate.transform.FindChild("desert gate open").gameObject.SetActive(true);
		gate.GetComponent<NavMeshObstacle>().carving = false;
	}

	public void LockGate(GameObject gate) {
		CmdLockGate(gate);
	}

	[Command]
	private void CmdLockGate(GameObject gate) {
		RpcLockGate(gate);
	}

	[ClientRpc]
	private void RpcLockGate(GameObject gate) {
		gate.transform.FindChild("desert gate").gameObject.SetActive(true);
		gate.transform.FindChild("desert gate open").gameObject.SetActive(false);
		gate.GetComponent<NavMeshObstacle>().carving = true;
	}
}
