/* Zoilo Mercedes
 * Ball Actions
 * Checks for cheating, collides with collectibles
 */
using UnityEngine;
using System;

public class BallActions : MonoBehaviour {

	// ball info
	private Vector3 startPosition;
	private bool cheating = false;
	private bool isGrabbed = false;
	private Rigidbody rigidBody;
	private bool colliding;
	public float maxAngVel;

	// collectibles reference
	public CollectibleSetter collectibles;

	// audio
	private AudioSource ballRoll;
	private AudioSource ballLand;
	private AudioSource starCollect;
	private AudioSource fail;

	// action taken on meeting win condition
	public event Action winCon;

	void Awake () {
		AudioSource[] sources = GetComponents<AudioSource>();
		starCollect = sources[0];
		ballLand = sources[1];
		ballRoll = sources[2];
		fail = sources[3];
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.maxAngularVelocity = maxAngVel;
		startPosition = transform.position;
	}
	
	void Update () {
		// when parent != null, ball is being grabbed
		isGrabbed = transform.parent != null;
		
		RollCheck();

		if(isGrabbed)
			CheatCheck();
	}

	void RollCheck(){
		if(rigidBody.angularVelocity.magnitude > 0.1f && !ballRoll.isPlaying && colliding)
			ballRoll.Play();
		else if(rigidBody.angularVelocity.magnitude < 0.1f || !colliding)
			ballRoll.Stop();
	}

	void CheatCheck(){
		RaycastHit hit;
		int mask = 1 << 8;

		// if collider below is not "platform", isCheating 
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 15f, mask))
			cheating = hit.collider.tag != "platform";
	}

	void Reset(){
		ResetVelocity();
		fail.Play();
		transform.position = startPosition;
		collectibles.Reset();
		cheating = false;
	}

	void ResetVelocity(){
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
	}

	void OnCollisionEnter(Collision col){
		colliding = true;
		if(col.gameObject.tag == "teleport" || cheating)
			Reset();

		ballLand.Play();

		if(col.gameObject.tag == "target"){
			if(collectibles.Collected()){
				ResetVelocity();
				winCon();
			} else
				Reset();
		}	
	}

	void OnCollisionExit(){
		colliding = false;
		ballRoll.Stop();
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "collect"){
			starCollect.Play();
			Destroy(col.gameObject);
		}
	}
}
