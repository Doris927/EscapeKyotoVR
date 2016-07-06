using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour {

	public Transform[] points;

	void OnDrawGizmos(){
		iTween.DrawPath (points);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
