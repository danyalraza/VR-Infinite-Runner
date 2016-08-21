using UnityEngine;
using System.Collections;

public class ChunkController : MonoBehaviour {
	public GameObject playerObject;
	public float playerPos;
	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find("Main Camera");
		playerPos = playerObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		playerPos = playerObject.transform.position.z;
		if (playerPos > (gameObject.transform.position.z + 100f)) {
			Destroy(gameObject);
		}
	}
}
