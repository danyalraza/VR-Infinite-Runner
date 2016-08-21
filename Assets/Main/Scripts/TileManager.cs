using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {
	public GameObject nextChunkPrefab;
	public GameObject currentChunk;
	public GameObject[] levelTiles;
	public float currentpos;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			SpawnChunk ();
		}
		currentpos = 0;
	}

	// Update is called once per frame
	void FixedUpdate () {
		GameObject playerObject = IntroLevel.player;
		if (PhotonNetwork.isMasterClient && playerObject != null) {
			Vector3 playerPos = playerObject.transform.position;
			currentpos += playerObject.GetComponent<Rigidbody> ().velocity.z * Time.fixedDeltaTime;
			if (currentpos > 50) {
				SpawnChunk ();
				currentpos = 0;
			}
		}
	}

	public void SpawnChunk() {
		int randomTileIndex = Random.Range (0, levelTiles.Length);
		nextChunkPrefab = levelTiles [randomTileIndex];
		currentChunk = (GameObject) PhotonNetwork.Instantiate (nextChunkPrefab.name, currentChunk.transform.GetChild(0).position, Quaternion.identity, 0);
	}
}
