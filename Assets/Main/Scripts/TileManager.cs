using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {
	public GameObject nextChunkPrefab;
	public GameObject currentChunk;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			SpawnChunk ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SpawnChunk() {
		currentChunk = (GameObject) Instantiate (nextChunkPrefab, currentChunk.transform.GetChild(0).position, Quaternion.identity);
	}
}
