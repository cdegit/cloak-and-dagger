using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections.Generic;
using UnityEngine.UI;

// Referenced http://docs.unity3d.com/Manual/UNetInternetServicesOverview.html

public class LobbyUI : MonoBehaviour {
	public GameObject matchButtonPrefab;

	List<MatchDesc> matchList = new List<MatchDesc>();
	bool matchCreated;
	NetworkMatch networkMatch;

	string connectionStatus = "";

	int uuid = 0;

	void Awake() {
		networkMatch = gameObject.AddComponent<NetworkMatch>();
	}

	// Poll to check matches
	void Start() {
		StartCoroutine("CheckMatches");
	}

	void OnGUI() {
		GameObject.Find("Connection Status").GetComponent<Text>().text = connectionStatus;
	}

	IEnumerator CheckMatches() {
		yield return new WaitForSeconds(0.5f);
		networkMatch.ListMatches(0, 20, "", OnMatchList);
		StartCoroutine("CheckMatches");
	}

	public void CreateMatch() {
		CreateMatchRequest create = new CreateMatchRequest();
		create.name = "NewRoom " + uuid; // append with some uuid
		create.size = 2;
		create.advertise = true;
		create.password = "";

		uuid++;

		networkMatch.CreateMatch(create, OnMatchCreate);
	}

	public void OnMatchCreate(CreateMatchResponse matchResponse)
	{
		if (matchResponse.success) {
			connectionStatus = "Match created. Waiting for another player to join...";

			transform.FindChild("CreatedPanel").gameObject.SetActive(true);
			transform.FindChild("CreatePanel").gameObject.SetActive(false);
			transform.FindChild("JoinPanel").gameObject.SetActive(false);

			matchCreated = true;
			Utility.SetAccessTokenForNetwork(matchResponse.networkId, new NetworkAccessToken(matchResponse.accessTokenString));
			NetworkServer.Listen(new MatchInfo(matchResponse), 7777); // 9000
			NetworkServer.RegisterHandler(MsgType.Connect, OnPlayerReadyMessage);
		} else {
			Debug.LogError ("Create match failed");
		}
	}

	public void OnPlayerReadyMessage(NetworkMessage netMsg) {
		Debug.Log("Player ready");

		connectionStatus = "Starting Game";

		NetworkManager.singleton.StartHost(NetworkManager.singleton.matchInfo);
		NetworkManager.singleton.ServerChangeScene("microLevelA");
	}

	public void OnMatchList(ListMatchResponse matchListResponse)
	{
		if (matchListResponse.success && matchListResponse.matches != null)
		{
			matchList = matchListResponse.matches;

			Transform joiningPanel = transform.FindChild("JoiningPanel").FindChild("MatchesPanel");

			foreach (Transform child in joiningPanel) {
				Destroy(child.gameObject);
			}

			foreach (var match in matchList) {
				string buttonText = "Join match: " + match.name;

				GameObject goButton = (GameObject)Instantiate(matchButtonPrefab);
				goButton.transform.SetParent(joiningPanel, false);
				goButton.transform.FindChild("Text").GetComponent<Text>().text = buttonText;

				Button tempButton = goButton.GetComponent<Button>();
				tempButton.onClick.AddListener(() => networkMatch.JoinMatch(match.networkId, "", OnMatchJoined));
			}
		}
	}

	public void OnMatchJoined(JoinMatchResponse matchJoin)
	{
		if (matchJoin.success)
		{
			connectionStatus = "Joined Match";
			Debug.Log("Join match succeeded");
			if (matchCreated)
			{
				connectionStatus = "Error";
				Debug.LogWarning("Match already set up, aborting...");
				return;
			}
			Utility.SetAccessTokenForNetwork(matchJoin.networkId, new NetworkAccessToken(matchJoin.accessTokenString));
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			myClient.Connect(new MatchInfo(matchJoin));
		}
		else
		{
			Debug.LogError("Join match failed");
		}
	}

	public void OnConnected(NetworkMessage msg)
	{
		connectionStatus = "Connected";
		Debug.Log("Connected!");
		NetworkManager.singleton.StartClient(NetworkManager.singleton.matchInfo);
	}
}