using UnityEngine;
using System.Collections;

public class WaitingRoom : MonoBehaviour {
	const string ROOM_NAME = "ROOM";
	const int PLAYERS_TO_START = 2;
	bool connected = false;

	void Start () {
	}
	
	void Update () {
		if (Input.GetMouseButtonUp (0) && !connected) {
			onReady ();
		}
		if (connected) {
			Debug.Log (PhotonNetwork.room.playerCount + " PLAYERS");
		}
		if (connected && PhotonNetwork.room.playerCount > PLAYERS_TO_START) {
			startGame ();
		}
	}

	void onReady() {
		TextMesh mesh = (TextMesh) GetComponent (typeof(TextMesh));
		mesh.text = "Waiting for other players...";
		PhotonNetwork.ConnectUsingSettings("0.1");
		Debug.Log ("Connected");
	}

	void OnJoinedLobby() {
		Debug.Log("joined lobby");
		RoomOptions roomOptions = new RoomOptions() { };
		PhotonNetwork.JoinOrCreateRoom(ROOM_NAME, roomOptions, TypedLobby.Default);
	}


	void OnJoinedRoom() {
		Debug.Log ("JOINED ROOM!");
		Debug.Log ("Offline mode: " + PhotonNetwork.offlineMode);
		Debug.Log ("Connected: " + PhotonNetwork.connected);
		// PhotonNetwork.Instantiate("NetworkedPlayer", Vector3.zero, Quaternion.identity, 0);
		int players = PhotonNetwork.room.playerCount;
		Debug.Log ("There are " + players + " players");
		if (players >= PLAYERS_TO_START) {
			startGame ();
		}
		connected = true;
	}

	void startGame(){
		Application.LoadLevel ("IntroScene");
	}

	void OnDestroy(){
		PhotonNetwork.Disconnect ();
		Debug.Log ("DISCONNECTED!");
	}
}
