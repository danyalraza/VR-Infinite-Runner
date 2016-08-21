using UnityEngine;
using System.Collections;

public class ChunkController : MonoBehaviour {
	public float playerPos = -999;
	
	// Update is called once per frame
	void Update () {
		GameObject playerObject = IntroLevel.player;
		if (playerObject != null) {
			if (playerPos == -999) {
				playerPos = playerObject.transform.position.z;
			} else {
				playerPos = playerObject.transform.position.z;
				if (playerPos > (gameObject.transform.position.z + 50f)) {
					Destroy (gameObject);
				}
			}
		}
	}
}
