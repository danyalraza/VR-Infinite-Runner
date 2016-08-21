using UnityEngine;
using System.Collections;

public class WaitingRoom : MonoBehaviour {
	const string ROOM_NAME = "THE_ROOM";
	const int PLAYERS_TO_START = 1;
	bool connected = false;
	bool switchLevels = false;

	void Start () {
	}
	
	void Update () {
		if (Input.GetMouseButtonUp (0) && !connected) {
			onReady ();
		}
		if (connected) {
			Debug.Log (PhotonNetwork.room.playerCount + " PLAYERS");
		}
		if (connected && PhotonNetwork.room.playerCount >= PLAYERS_TO_START) {
			startGame ();
		}
	}

	void onReady() {
		PhotonNetwork.ConnectUsingSettings("0.1");
		TextMesh mesh = (TextMesh) GetComponent (typeof(TextMesh));
		mesh.text = "Waiting for other players...";
		Debug.Log ("Connected");
	}

	void OnJoinedLobby() {
		Debug.Log("joined lobby");
		RoomOptions roomOptions = new RoomOptions() { };
		PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, TypedLobby.Default);
	}


	void OnJoinedRoom() {
		int players = PhotonNetwork.room.playerCount;
		if (players >= PLAYERS_TO_START) {
			startGame ();
		}
		connected = true;
	}

	void startGame(){
		Debug.Log ("Started GAME: " + PhotonNetwork.room);
		switchLevels = true;
		PhotonNetwork.isMessageQueueRunning = false;
		Application.LoadLevel ("IntroScene");
	}

	void OnLevelWasLoaded(){
		PhotonNetwork.isMessageQueueRunning = true;
		Debug.Log ("Loaded!");
	}

	void OnDestroy(){
		if (connected && !switchLevels) {
			PhotonNetwork.Disconnect ();
			Debug.Log ("DISCONNECTED!");
		}
	}
}
