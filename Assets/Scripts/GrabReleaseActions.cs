using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabReleaseActions : MonoBehaviour {
	
	LeftControllerInput leftInput;
	RightControllerInput rightInput;

	public float throwForce = 1.5f;

	// Use this for initialization
	void Awake () {
		leftInput = GameObject.Find("Controller (left)").GetComponent<LeftControllerInput>();
		rightInput = GameObject.Find("Controller (right)").GetComponent<RightControllerInput>();
		
		leftInput.GrabAction += GrabObject;
		rightInput.GrabAction += GrabObject;
		leftInput.ReleaseAction += PlaceOrThrow;
		rightInput.ReleaseAction += PlaceOrThrow;
	}
	
	void PlaceorThrow(Collider col, SteamVR_Controller.Device controller, string tag){
		switch (tag){
			case 1:
			break;
          case "place:
				Console.WriteLine("Case 2");
				break;
          default:
              Console.WriteLine("Default case");
              break;
      }
			PlaceObject(col, controller);
		else
			ThrowObject(col, controller);
	}
	
	void GrabObject(Collider coll, SteamVR_Controller.Device controller, Transform t){
		coll.transform.SetParent(t);                        // make controller parent
		coll.GetComponent<Rigidbody>().isKinematic = true;  // turn off physics
		controller.TriggerHapticPulse(2000);		    // vibrate controller
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

	void PlaceObject(Collider col, SteamVR_Controller.Device controller){
		// do stuff
	}
}
