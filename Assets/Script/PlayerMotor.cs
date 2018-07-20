using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {
	private Touch initialTouch = new Touch();
	private float distance = 0;
	private bool hasSwiped = false;
	Vector3 moveVector;
	public float speed = 1.0f;
	float distToGround;
	public float jumpSpeed;
	float vVelocity;
	public float gravity=.3f;
	bool isDeath;
	CharacterController cc;
	Animator anim;
	Quaternion quaten=Quaternion.identity;
	int currentRot;
	int runState;
	int turningSpeed=100000;

	void Start () {
		currentRot = (int)this.transform.rotation.eulerAngles.y;
		//Debug.Log (currentRot);
		cc = GetComponent<CharacterController> ();
		distToGround = cc.height / 2;//cc.bounds.extents.y/2;
		//Debug.Log(cc.height);
		anim = GetComponent<Animator> ();
		isDeath=false;
		runState=Animator.StringToHash("Base Layer.SprintRunning");

	}
	void FixedUpdate()
	{
		foreach(Touch t in Input.touches)
		{
			if (t.phase == TouchPhase.Began)
			{
				initialTouch = t;
			}
			else if (t.phase == TouchPhase.Moved && !hasSwiped)
			{
				float deltaX = initialTouch.position.x - t.position.x;
				float deltaY = initialTouch.position.y - t.position.y;
				distance = Mathf.Sqrt((deltaX * deltaX) + (deltaY * deltaY));
				bool swipedSideways = Mathf.Abs(deltaX) > Mathf.Abs(deltaY);

				if (distance > 100f)
				{
					if (swipedSideways && deltaX > 0  ||  Input.GetKey(KeyCode.LeftArrow) ) //swiped left
					{
						currentRot-=90;
						quaten = Quaternion.Euler(0f,currentRot, 0f);
						transform.rotation = Quaternion.RotateTowards(transform.rotation, quaten,turningSpeed * Time.deltaTime);
						//this.transform.Rotate(new Vector3(0, -15f, 0));
					}
					else if (swipedSideways && deltaX <= 0 || Input.GetKey(KeyCode.RightArrow)) //swiped right
					{
						currentRot+=90;
						quaten = Quaternion.Euler(0f,currentRot, 0f);
						transform.rotation = Quaternion.RotateTowards(transform.rotation, quaten,turningSpeed * Time.deltaTime);
						//this.transform.Rotate(new Vector3(0, 15f, 0));
					}
					else if (!swipedSideways && deltaY > 0  || Input.GetKey(KeyCode.DownArrow)) //swiped down
					{
						anim.SetTrigger ("Slide");
						//this.transform.Rotate(new Vector3(0, 180f, 0));
					}
					else if (!swipedSideways && deltaY <= 0 || Input.GetKey(KeyCode.UpArrow))  //swiped up
					{
						anim.SetTrigger ("Jump"); 
						
						//this.rigidbody.velocity = new Vector3(this.rigidbody.velocity.x, 0, this.rigidbody.velocity.z);
						//this.rigidbody.AddForce(new Vector3(0, 100f, 0));
					}

					hasSwiped = true;
				}

			}
			else if (t.phase == TouchPhase.Ended)
			{
				initialTouch = new Touch();
				hasSwiped = false;
			}
		}
	}







	void Update () {
		
		//Debug.Log (Input.GetAxis("Vertical"));
		

		if(isDeath){
			
			return;
		}
		AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo (0);




		/*******************************************User Iput Are Here/Player Controler Code ******************************************/

		/*if (Input.GetKeyDown (KeyCode.Space)) {
			//Left Rotation,Left Move
			currentRot-=90f;
			quaten = Quaternion.Euler(0f,currentRot, 0f);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, quaten,turningSpeed * Time.deltaTime);
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			//Right Rotation,Right Move
			currentRot+=90;
			quaten = Quaternion.Euler(0f,currentRot, 0f);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, quaten,turningSpeed * Time.deltaTime);
		} 

		if (Input.GetKeyDown (KeyCode.Space) && currentState.nameHash==runState) {
			//Slider Input
			anim.SetTrigger ("Slide");
	        } 
		if (Input.GetKeyDown (KeyCode.Space) && currentState.nameHash==runState) {
		   //Jump Input
		    anim.SetTrigger ("Jump"); 
		   } 
         */












		if (IsGrounded()) {
			vVelocity = 0.0f;
			
		}
		else {
			//vVelocity -= gravity*Time.deltaTime;
			
		}
		moveVector.x = Input.GetAxisRaw("Horizontal")*speed*Time.deltaTime;
		moveVector.y = vVelocity;
	    transform.Translate(moveVector);
	    transform.Translate (Vector3.forward*speed*Time.deltaTime);

}




 bool IsGrounded() {
		Debug.DrawRay(transform.position, -Vector3.up, Color.green);
		return Physics.Raycast(transform.position, -Vector3.up,.1f/*distToGround */);
}





void androidControler(){
		//Do Jump
		//
		
}


	void OnControllerColliderHit(ControllerColliderHit Hit){
		if (Hit.gameObject.CompareTag ("Enemy") && !isDeath) {
			isDeath = true;
			moveVector.z -= 1.0f;
			transform.Translate (moveVector * Time.deltaTime);
				anim.SetTrigger ("Die");


		}


	}

	

}
		