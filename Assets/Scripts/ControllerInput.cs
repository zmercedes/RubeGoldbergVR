using UnityEngine;
using System;
using System.Linq;

public class ControllerInput : MonoBehaviour {

	// controller references
	private SteamVR_Controller.Device controller;
	private SteamVR_TrackedObject trackedObj;

	private string[] grabableObjects = {"place","throw"};

	void ReleaseActionPicker(Collider col, SteamVR_Controller.Device controller, string tag){
		switch (tag){
			case "place":
				PlaceObject(col,controller);
				break;
			case "throw":
				ThrowObject(col,controller);
				break;
			default:
				Debug.Log("Nothing here!");
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

	void PlaceObject(Collider col){
		col.transform.SetParent(null);
	}
}
