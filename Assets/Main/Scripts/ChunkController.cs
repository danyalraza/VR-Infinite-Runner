using UnityEngine;
using System.Collections;

public class ChunkController : MonoBehaviour {
	public GameObject playerObject;
	public float playerPos;
	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find("Tank");
		playerPos = playerObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = playerObject.transform.position.z;
		if (playerPos > (gameObject.transform.position.z + 50f)) {
			Destroy(gameObject);
		}
	}
}
