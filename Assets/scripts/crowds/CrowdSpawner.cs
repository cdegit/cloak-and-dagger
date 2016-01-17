using UnityEngine;
using UnityEngine.Networking;

public class CrowdSpawner : NetworkBehaviour {

    public GameObject NPCPrefab;
    public int numNPCs;

    public override void OnStartServer() {
        for (int i = 0; i < numNPCs; i++)
        {
            var pos = new Vector3(
                Random.Range(-8.0f, 8.0f),
                0.2f,
                Random.Range(-8.0f, 8.0f)
            );
            var rotation = Quaternion.Euler(0, 0, 0);
            var npc = (GameObject)Instantiate(NPCPrefab, pos, rotation);

            NetworkServer.Spawn(npc);
        }
    }
}
