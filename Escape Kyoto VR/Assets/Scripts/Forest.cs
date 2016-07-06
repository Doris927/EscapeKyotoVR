using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {

	public int th=20; // threshold for destroying the forest 
	public int forestLength=600; // length of the forest 
	public float startLength=50; // start length for generating obstacles
	public float minLength=40; // the minmum length for generating obstacles
	public float maxLength=70; // the maxmum length for generating obstacles 


	private Transform player;//Player 
	public GameObject[] obstacles;//Obstacles
	private WayPoints wayPoints;//WayPoints
	private int targetWayPointIndex;
	private EnvGenerator envGenerator;

	void Awake(){
		player = GameObject.FindGameObjectWithTag (Tags.player).transform;
		wayPoints = transform.Find ("WayPoints").GetComponent<WayPoints> ();
		targetWayPointIndex = wayPoints.points.Length - 2;
		envGenerator = player.GetComponent<EnvGenerator> ();
	}

	// Use this for initialization
	void Start () {
		GenerateObstacles ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (player.position.z > (this.transform.position.z+th)) {
//			envGenerator.GenerateForest ();
//			GameObject.Destroy (this.gameObject);
//		}
	}

	//generate obstacles randomly
	void GenerateObstacles(){
		float startZ = transform.position.z - forestLength;
		float endZ = transform.position.z;
		float tempz = startZ + startLength;
		while (true) {
			tempz += Random.Range (minLength, maxLength);
			if (tempz > endZ) {
				break;
			} else {
				Vector3 position = GetWayPosByZ (tempz);
				//generate obstacles
				int type=Random.Range(0,obstacles.Length);
				GameObject go=GameObject.Instantiate (obstacles [type], position, Quaternion.identity) as GameObject;
				go.transform.parent = this.transform;
			}
		}
	}

	//get the waypoint position by z
	Vector3 GetWayPosByZ(float z){
		Transform[] points = wayPoints.points;
		int index = 0;
		for (int i = 0; i < points.Length-1; i++) {
			if (z <= points [i].position.z && z >= points [i + 1].position.z) {
				index = i;
				break;
			}
		}

		//index index+1
		return Vector3.Lerp(points[index+1].position,points[index].position,
			(z-points[index+1].position.z)/(points[index].position.z-points[index+1].position.z));
	}

	//Get next point
	public Vector3 GetNextTargetPoint(){
		int distance =1;

		if (targetWayPointIndex < 0) {
			return envGenerator.forest1.GetNextTargetPoint ();
		}

		while (true) {
			print ("waypoint_z:"+wayPoints.points [targetWayPointIndex].position.z);
			if (wayPoints.points [targetWayPointIndex].position.z - player.position.z < distance) {
				targetWayPointIndex--;
				//print (targetWayPointIndex);
				if (targetWayPointIndex < 0) {
					envGenerator.GenerateForest ();
					Destroy (this.gameObject,2);
					return envGenerator.forest1.GetNextTargetPoint ();
				}
			} else {
				return wayPoints.points [targetWayPointIndex].position;
			}
		}

	}
}
