using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		Text text = GameObject.Find("Score").GetComponent<Text> ();
		text.text = "Score:"+Settings.score;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
