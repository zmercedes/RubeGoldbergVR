using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabReleaseActions : MonoBehaviour {

	public float throwForce = 1.5f;

	// Use this for initialization
	void Awake () {
		GameObject left = GameObject.Find("Controller (left)");
		GameObject right = GameObject.Find("Controller (right)");

		left.GetComponent<LeftControllerInput>().GrabAction += GrabObject;
		left.GetComponent<LeftControllerInput>().ReleaseAction += ThrowObject;
		right.GetComponent<RightControllerInput>().GrabAction += GrabObject;
		right.GetComponent<RightControllerInput>().ReleaseAction += ThrowObject;
		
	}
	
	void GrabObject(Collider coll, SteamVR_Controller.Device controller, Transform t){
		coll.transform.SetParent(t);     // make controller parent
		coll.GetComponent<Rigidbody>().isKinematic = true;  // turn off physics
		controller.TriggerHapticPulse(2000);				// vibrate controller
		Debug.Log("Grabbing object!");
	}

	void ThrowObject(Collider coll, SteamVR_Controller.Device controller){
		coll.transform.SetParent(null);
		Rigidbody rigidBody = coll.GetComponent<Rigidbody>();
		rigidBody.isKinematic = false;
		rigidBody.velocity = controller.velocity * throwForce;
		rigidBody.angularVelocity = controller.angularVelocity;
		Debug.Log("Released object!");
	}

	void PlaceObject(){
		
	}
}
