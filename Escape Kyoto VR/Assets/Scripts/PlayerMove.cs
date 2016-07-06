using UnityEngine;
using System.Collections;

public enum TouchDir{
	None,
	Left,
	Right,
	Top,
	Bottom
}

public class PlayerMove : MonoBehaviour {

	public float moveSpeed = 20;
	public float moveHSpeed = 2;
	public bool isSliding = false;
	public float slideTime = 1.45f;
	public bool isJumping = false;
	public float jumpHeight=20f;
	public float jumpSpeed=40f;

	private float currentJumpHeight=0;
	public bool isUp = true;
	private float slideTimer = 0;
	private EnvGenerator envGenerator;
	private TouchDir touchDir = TouchDir.None;
	private Vector3 lastMouseDown = Vector3.zero;
	public int nowLaneIndex = 1;//current lane 
	public int targetLaneIndex=1;//target lane 
	private float moveHDis=0;
	public float[] xOffset=new float[3]{-2.3f,0,2.3f};
	private Transform prisoner;



	void Awake(){
		envGenerator = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<EnvGenerator> ();
//		prisoner = this.transform.Find ("Prisoner").transform;
	}

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (GameController.gameState == GameState.Playing) {
			Vector3 targetPosition = envGenerator.forest1.GetNextTargetPoint ();
			targetPosition = new Vector3 (targetPosition.x + xOffset [targetLaneIndex], targetPosition.y,
				targetPosition.z);
			Vector3 moveDir = targetPosition - transform.position;
			transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;

			MoveControl ();
		} else {
			if (Input.GetMouseButtonDown(0)) {
				lastMouseDown = Input.mousePosition;
			}
		}
	}

	private void MoveControl(){
		TouchDir dir = GetTouchDir ();
		if (dir != TouchDir.None) {
//			print (dir);
		}
		if (targetLaneIndex != nowLaneIndex) {
			float moveLength = Mathf.Lerp (0, moveHDis,Time.deltaTime*moveHSpeed);
			transform.position = new Vector3 (transform.position.x+moveLength,transform.position.y,
				transform.position.z);
			moveHDis -= moveLength;
			float threshold = 0.5f;
			if (Mathf.Abs(moveHDis) < threshold) {
				transform.position = new Vector3 (transform.position.x+moveHDis,transform.position.y,
					transform.position.z);
				moveHDis = 0;
				nowLaneIndex = targetLaneIndex;
			}
		}

		//slide
		if(isSliding){
			slideTimer += Time.deltaTime;
			if (slideTimer >= slideTime) {
				slideTimer = 0;
				isSliding = false;
			}
		}

		//jump
		if(isJumping){
			float yMove = jumpSpeed * Time.deltaTime;
			float threshold = 0.5f;
			if (isUp) {
				prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y + yMove, 
					prisoner.position.z);
				currentJumpHeight += yMove;
				if (Mathf.Abs (jumpHeight - currentJumpHeight) < threshold) {
					prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y + jumpHeight - currentJumpHeight, 
						prisoner.position.z);
					isUp = false;
					currentJumpHeight = jumpHeight;
				}
			} else {
				prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y - yMove, 
					prisoner.position.z);
				currentJumpHeight -= yMove;
				if (Mathf.Abs (currentJumpHeight) < threshold) {
					prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y - currentJumpHeight, 
						prisoner.position.z);
					isJumping = false;
					currentJumpHeight = 0;
				}
			}
		}
	}

	private TouchDir GetTouchDir(){
		if (Input.GetMouseButtonDown (0)) {
			lastMouseDown = Input.mousePosition;
		} else if (Input.GetMouseButtonUp (0)) {
			Vector3 mouseUp = Input.mousePosition;
			Vector3 touchOffset = mouseUp - lastMouseDown;
			float threshold = 0.5f;
			if (Mathf.Abs (touchOffset.x) > threshold||Mathf.Abs (touchOffset.y) > threshold) {
				if (Mathf.Abs (touchOffset.x) > Mathf.Abs (touchOffset.y)) {
					if (touchOffset.x > 0) {
						if (targetLaneIndex < 2) {
							targetLaneIndex++;
							moveHDis = 2.3f;
						}
						return TouchDir.Right;
					} else {
						if (targetLaneIndex > 0) {
							targetLaneIndex--;
							moveHDis = -2.3f;
						}
						return TouchDir.Left;
					}
				}
			}



		}


		Vector3 dir = Vector3.zero;
		dir.x = Input.acceleration.x;
		dir.y = Input.acceleration.y;


		float thAcceleration = 0.1f;
		if (Mathf.Abs (dir.x) > thAcceleration) {
//			if (Mathf.Abs (dir.x) >= Mathf.Abs (dir.y)) {
				if (dir.x > 0) {
					if (targetLaneIndex < 2) {
						targetLaneIndex++;
						moveHDis = 2.3f;
					}
					return TouchDir.Right; 
				} else {
					if (targetLaneIndex > 0) {
						targetLaneIndex--;
						moveHDis = -2.3f;
					}
					return TouchDir.Left;
				}
//			} 
//			else {
//				if (dir.y > 0) {
//					if (isJumping == false) {
//						isJumping = true;
//						isUp = true;
//						currentJumpHeight = 0;
//					}
//					return TouchDir.Top;
//				} 
//			}
		}
		return TouchDir.None;
	}
}
