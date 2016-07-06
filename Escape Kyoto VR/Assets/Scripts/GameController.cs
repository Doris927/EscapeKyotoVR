using UnityEngine;
using System.Collections;

public enum GameState{
	Menu,
	Playing,
	End
}

public class GameController : MonoBehaviour {

	public static GameState gameState=GameState.Menu;
	public GameObject tapToStartUI;
	public GameObject gameOverUI;

	public float allTime = 3;  
	public float countTime;  

	// Use this for initialization
	void Awake () {
		print ("awake");
		allTime = Time.time + allTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameState == GameState.Menu) {
			countTime = allTime - Time.time;
			if (countTime < 0) {
				gameState = GameState.Playing;
			}
		}
		if (gameState == GameState.End) {
			gameState = GameState.Menu;
			Application.LoadLevel ("end_scene");
		}
		if (gameState == GameState.Playing) {
			Settings.score = Mathf.RoundToInt(Time.time-allTime);
		}
	}
}
