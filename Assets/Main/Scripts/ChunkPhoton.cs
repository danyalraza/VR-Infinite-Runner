using UnityEngine;
using System.Collections;

public class ChunkPhoton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		// TODO: This should notify deletion
	}
}
