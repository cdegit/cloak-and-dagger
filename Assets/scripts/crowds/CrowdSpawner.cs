using UnityEngine;
using UnityEngine.Networking;

public class CrowdSpawner : NetworkBehaviour {

    public GameObject NPCPrefab;
    public int numNPCs;

    public override void OnStartServer() {
        var NPCHeight = NPCPrefab.GetComponent<Renderer>().bounds.size.y;

        for (int i = 0; i < numNPCs; i++)
        {
            // TODO: Set specific crowd spawn locations
            var pos = new Vector3(
                Random.Range(-8.0f, 8.0f),
                NPCHeight / 2, // This places them just above the ground
                Random.Range(-8.0f, 8.0f)
            );
            var rotation = Quaternion.Euler(0, 0, 0);
            var npc = (GameObject)Instantiate(NPCPrefab, pos, rotation);

            NetworkServer.Spawn(npc);
        }
    }
}
