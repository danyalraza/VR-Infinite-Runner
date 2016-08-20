using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {


	private Vector3 moveDirection = Vector3.zero;
	public float gravity = 20f;
	private CharacterController controller;

	public float JumpSpeed = 8.0f;
	public float Speed = 6.0f;
//	public Transform CharacterGO;

	bool isInSwipeArea;
	// Use this for initialization
	void Start () {
		controller = (CharacterController) GetComponent(typeof(CharacterController));
		moveDirection = transform.forward;
		moveDirection = transform.TransformDirection(moveDirection);
		moveDirection *= Speed;
	}

	// Update is called once per frame
	void Update () {
		controller.Move(moveDirection * Time.deltaTime);
	}
	private void CheckHeight()
	{
//		if (transform.position.y < -10)
//		{
//			GameManager.Instance.Die();
//		}
	}
}