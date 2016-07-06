using UnityEngine;
using System.Collections;

public class EnvGenerator : MonoBehaviour {

	public Forest forest1;
	public Forest forest2;
	public int forestCount = 2;
	public int forestWidth = 600;

	public GameObject[] forests;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GenerateForest(){
		forestCount++;
		int type = Random.Range (0, forests.Length);
		//print (type);
		GameObject newForest =( GameObject.Instantiate (forests [type], new Vector3 (0, 0, forestCount * forestWidth), Quaternion.identity) )as GameObject;
		forest1 = forest2;
		forest2 = newForest.GetComponent<Forest> ();
	}
}
