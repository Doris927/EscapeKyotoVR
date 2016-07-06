using UnityEngine;
using System.Collections;

public class PlayerSmallCollider : MonoBehaviour {

	private PlayerAnimation playerAnimation;

	void Awake(){
		playerAnimation = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerAnimation>();

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == Tags.obstacle && GameController.gameState==GameState.Playing) {
			GameController.gameState = GameState.End;
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
