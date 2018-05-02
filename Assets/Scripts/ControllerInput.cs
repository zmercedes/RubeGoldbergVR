/* Zoilo Mercedes
 * Controller Input Base Class
 */
using UnityEngine;
using System;
using System.Linq;

public class ControllerInput : MonoBehaviour {

	// controller references
	protected SteamVR_Controller.Device controller;
	protected SteamVR_TrackedObject trackedObj;

	// objects that can be grabbed 
	protected string[] grabableObjects = {"place","throw"};

	protected float throwForce = 1.5f;

	protected void ReleaseAction(Collider col){
		switch (col.gameObject.tag){
			case "place":
				PlaceObject(col);
				break;
			case "throw":
				ThrowObject(col);
				break;
			default:
				Debug.Log("Nothing here!");
				break;
		}
	}
	
	protected void GrabObject(Collider col, Transform t){
		col.transform.SetParent(t);                        // make controller parent
		col.GetComponent<Rigidbody>().isKinematic = true;  // turn off physics
		controller.TriggerHapticPulse(2000);               // vibrate controller
		Debug.Log("Grabbing object!");
	}

	protected void ThrowObject(Collider col){
		col.transform.SetParent(null);
		Rigidbody rigidBody = col.GetComponent<Rigidbody>();
		rigidBody.isKinematic = false;
		rigidBody.velocity = controller.velocity * throwForce;
		rigidBody.angularVelocity = controller.angularVelocity;
		Debug.Log("Released object!");
	}

	protected void PlaceObject(Collider col){
		col.transform.SetParent(null);
	}

	void OnTriggerStay(Collider col){
		if(grabableObjects.Contains(col.gameObject.tag)){
			if(controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
				ReleaseAction(col);
			else if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
				GrabObject(col, transform);
		}
	}
}
