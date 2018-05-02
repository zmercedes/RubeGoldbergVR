/* Zoilo Mercedes
 * GrabReleaseActions
 * Sets appropriate grab and release actions. Common code between both hands
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabReleaseActions : MonoBehaviour {

	// controller input references
	LeftControllerInput leftInput;
	RightControllerInput rightInput;

	public float throwForce = 1.5f;

	void Awake () {
		leftInput = transform.GetChild(0).GetComponent<LeftControllerInput>();
		rightInput = transform.GetChild(1).GetComponent<RightControllerInput>();
		
		leftInput.GrabAction += GrabObject;
		rightInput.GrabAction += GrabObject;
		leftInput.ReleaseAction += ReleaseActionPicker;
		rightInput.ReleaseAction += ReleaseActionPicker;
	}

	// does appropriate release action based on gameobject tag
	void ReleaseActionPicker(Collider col, SteamVR_Controller.Device controller, string tag){
		switch (tag){
			case "place":
				PlaceObject(col,controller);
				break;
			case "throw":
				ThrowObject(col,controller);
				break;
			default:
				Debug.Log("Nothing to grab here!");
				break;
		}
	}
	
	void GrabObject(Collider col, SteamVR_Controller.Device controller, Transform t){
		col.transform.SetParent(t);                        // make controller parent
		col.GetComponent<Rigidbody>().isKinematic = true;  // turn off physics
		controller.TriggerHapticPulse(2000);               // vibrate controller
		Debug.Log("Grabbing object!");
	}

	void ThrowObject(Collider col, SteamVR_Controller.Device controller){
		col.transform.SetParent(null);
		Rigidbody rigidBody = col.GetComponent<Rigidbody>();
		rigidBody.isKinematic = false;
		rigidBody.velocity = controller.velocity * throwForce;
		rigidBody.angularVelocity = controller.angularVelocity;
		Debug.Log("Released object!");
	}

	void PlaceObject(Collider col, SteamVR_Controller.Device controller){
		col.transform.SetParent(null);
		Rigidbody rigidBody = col.GetComponent<Rigidbody>();
		rigidBody.isKinematic = false;
	}
}