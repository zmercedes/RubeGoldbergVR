/* Zoilo Mercedes
 * Ball Actions
 * Checks for cheating, collides with collectibles
 */
using UnityEngine;
using System;

public class BallActions : MonoBehaviour {

	Vector3 startPosition;
	bool cheating = false;
	bool isGrabbed = false;
	Rigidbody rigidBody;
	public float maxAngVel;
	public CollectibleSetter collectibles;
	public event Action winCon;

	void Awake () {
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.maxAngularVelocity = maxAngVel;
		startPosition = transform.position;
	}
	
	void Update () {
		// when parent != null, ball is being grabbed
		isGrabbed = transform.parent != null;
		
		if(isGrabbed)
			CheatCheck();
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
		transform.position = startPosition;
		collectibles.Reset();
		cheating = false;
	}

	void ResetVelocity(){
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "teleport" || cheating)
			Reset();

		if(col.gameObject.tag == "target"){
			if(collectibles.Collected()){
				ResetVelocity();
				winCon();
			} else
				Reset();
		}	
	}

	void OnTriggerEnter(Collider col){
		if(col.tag == "collect")
			Destroy(col.gameObject);
	}
}
