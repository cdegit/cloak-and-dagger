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
	string matchName = "";

	int uuid = 0;

	MatchInfo matchInfo;

	Transform createPanel;
	Transform joinPanel;
	Transform joiningPanel;
	Transform matchesPanel;
	Transform createMatchButton;
	Transform joinMatchButton;

	Transform backButton;

	void Awake() {
		networkMatch = gameObject.AddComponent<NetworkMatch>();
	}

	// Poll to check matches
	void Start() {
		Transform parentPanel = transform.FindChild("ParentPanel");
		createPanel = parentPanel.FindChild("CreatePanel");
		joinPanel = parentPanel.FindChild("JoinPanel");
		joiningPanel = transform.FindChild("JoiningPanel");
		matchesPanel = joiningPanel.Find("MatchesPanel");
		createMatchButton = createPanel.FindChild("CreateMatchButton");
		joinMatchButton = joinPanel.FindChild("JoinMatchButton");

		backButton = transform.FindChild("BackButton");

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
		create.name = "Room " + uuid;
		create.size = 2;
		create.advertise = true;
		create.password = "";

		matchName = create.name;

		uuid++;

		networkMatch.CreateMatch(create, OnMatchCreate);
	}

	public void JoinMatch() {
		createPanel.FindChild("Image").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
		createMatchButton.gameObject.SetActive(false);
		joinMatchButton.gameObject.SetActive(false);
		joiningPanel.gameObject.SetActive(true);
		backButton.gameObject.SetActive(true);
	}

	public void OnMatchCreate(CreateMatchResponse matchResponse) {
		if (matchResponse.success) {
			connectionStatus = "Match named " + matchName + " created. Waiting for another player to join...";

			joinPanel.FindChild("Image").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
			createMatchButton.gameObject.SetActive(false);
			joinMatchButton.gameObject.SetActive(false);
			backButton.gameObject.SetActive(true);

			matchCreated = true;
			Utility.SetAccessTokenForNetwork(matchResponse.networkId, new NetworkAccessToken(matchResponse.accessTokenString));
			matchInfo = new MatchInfo(matchResponse);
			NetworkServer.Listen(matchInfo, 7777);
			NetworkServer.RegisterHandler(MsgType.Connect, OnPlayerReadyMessage);
		} else {
			Debug.LogError ("Create match failed");
		}
	}

	public void OnPlayerReadyMessage(NetworkMessage netMsg) {
		connectionStatus = "Starting Game...";

		NetworkManager.singleton.StartHost(matchInfo);
		NetworkManager.singleton.ServerChangeScene("microLevelA");
	}

	public void OnMatchList(ListMatchResponse matchListResponse) {
		if (matchListResponse.success && matchListResponse.matches != null)
		{
			matchList = matchListResponse.matches;

			foreach (Transform child in matchesPanel) {
				Destroy(child.gameObject);
			}

			foreach (var match in matchList) {
				string buttonText = "Join match: " + match.name;

				GameObject goButton = (GameObject)Instantiate(matchButtonPrefab);
				goButton.transform.SetParent(matchesPanel, false);
				goButton.transform.FindChild("Text").GetComponent<Text>().text = buttonText;

				Button tempButton = goButton.GetComponent<Button>();
				tempButton.onClick.AddListener(() => networkMatch.JoinMatch(match.networkId, "", OnMatchJoined));
			}
		}
	}

	public void OnMatchJoined(JoinMatchResponse matchJoin) {
		if (matchJoin.success) {
			connectionStatus = "Joined Match. Connecting...";

			joiningPanel.gameObject.SetActive(false);

			if (matchCreated) {
				connectionStatus = "Error. Cannot connect.";
				return;
			}

			Utility.SetAccessTokenForNetwork(matchJoin.networkId, new NetworkAccessToken(matchJoin.accessTokenString));
			NetworkClient myClient = new NetworkClient();
			myClient.RegisterHandler(MsgType.Connect, OnConnected);
			matchInfo = new MatchInfo(matchJoin);
			myClient.Connect(matchInfo);
		} else {
			connectionStatus = "Join match failed";
		}
	}

	public void OnConnected(NetworkMessage msg) {
		connectionStatus = "Connected. Starting game...";
		Debug.Log(matchInfo);
		Debug.Log(msg);

		NetworkManager.singleton.StartClient(matchInfo);
	}
}