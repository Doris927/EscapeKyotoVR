using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public float moveSpeed = 7;

	private Transform player;

	private Vector3 offset=Vector3.zero;

	void Awake(){
		player = GameObject.FindGameObjectWithTag (Tags.player).transform;
		offset = transform.position - player.position;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetPos = player.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
}
