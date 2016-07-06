using UnityEngine;
using System.Collections;



public class PlayerAnimation : MonoBehaviour {

	private Animation animation_;

	void Awake(){
		animation_ = this.GetComponent<Animation>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		PlayAnimation("Run");
	}





	private void PlayAnimation(string animName){
		if (animation_.IsPlaying (animName) == false) {
			animation_.Play (animName);
		}
	}

}
