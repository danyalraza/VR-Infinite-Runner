using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {
	public GameObject nextChunkPrefab;
	public GameObject currentChunk;
	public GameObject[] levelTiles;
	public float currentpos;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 15; i++) {
			SpawnChunk ();
		}
		currentpos = 0;
	}
		
	public void SpawnChunk() {
		int randomTileIndex = Random.Range (0, levelTiles.Length);
		nextChunkPrefab = levelTiles [randomTileIndex];
		currentChunk = (GameObject) Instantiate (nextChunkPrefab, currentChunk.transform.GetChild(0).position, Quaternion.identity);
	}
}
