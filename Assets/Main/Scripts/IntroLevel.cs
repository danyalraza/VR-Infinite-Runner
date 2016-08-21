using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroLevel : MonoBehaviour {

	private Vector3 startingPoint = new Vector3 (0, 0, 4);
	public static GameObject player = null;

	void Start () {
		player = PhotonNetwork.Instantiate (
			"Player",
			startingPoint,
			Quaternion.identity,
			1) as GameObject;
		Debug.Log ("PLAYER CREATED!: " + player == null);
	}
}