using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {
	public GameObject nextChunkPrefab;
	public GameObject currentChunk;
	public GameObject[] levelTiles;
	public float currentpos;
	private float x = 0;
	private float lastChunk = 0;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < 5; i++) {
			SpawnChunk ();
		}
		currentpos = 0;
	}
	void Update() {
		if (x > lastChunk + 1000) {
			SpawnChunk ();
			lastChunk = x;
		}
	}	
	public void SpawnChunk() {
		int randomTileIndex = Random.Range (0, levelTiles.Length);
		nextChunkPrefab = levelTiles [randomTileIndex];
		currentChunk = (GameObject) Instantiate (nextChunkPrefab, currentChunk.transform.GetChild(0).position, Quaternion.identity);
	}

	public void addX(float x){
		this.x += x;
	}
	public int getX() {
		return Mathf.FloorToInt (this.x);
	}
}
