﻿using UnityEngine;
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
		GameObject playerObject = GameObject.Find("Tank");
		Vector3 playerPos = playerObject.transform.position;
		currentpos += playerObject.GetComponent<Rigidbody>().velocity.z * Time.fixedDeltaTime;
		if (currentpos > 50) {
			SpawnChunk();
			currentpos = 0;
		}
	}

	public void SpawnChunk() {
		currentChunk = (GameObject) Instantiate (nextChunkPrefab, currentChunk.transform.GetChild(0).position, Quaternion.identity);
	}
}
