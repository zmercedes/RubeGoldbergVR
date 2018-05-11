/* Zoilo Mercedes
 * Controller Input Base Class
 */
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class ControllerInput : MonoBehaviour {

	// controller references, set in derived classes
	protected SteamVR_Controller.Device controller;
	protected SteamVR_TrackedObject trackedObj;

	// line renderer
	public LineRenderer line;

	// ui toggler
	public event Action UItoggle;

	// objects that can be grabbed 
	string[] grabableObjects = {"place","throw"};

	float throwForce = 1.5f;

	protected void UISelector(){
		int mask = 1 << 5;
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.forward, out hit, 15f, mask)){
			line.gameObject.SetActive(true);
			line.SetPosition(0, transform.position);
			line.SetPosition(1, hit.point);
			if(hit.collider.tag == "button"){
				Button button = hit.collider.gameObject.GetComponent<Button>();
				button.Select();
				// press button on trigger press
				if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
					hit.collider.gameObject.GetComponent<Button>().onClick.Invoke();
			} else
				EventSystem.current.SetSelectedGameObject(null); // deselects button
		} else
			line.gameObject.SetActive(false); // when not aiming at UI, turn off button
	}

	protected void Toggler(){
		UItoggle();
	}

	void ReleaseAction(Collider col){
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


	void GrabObject(Collider col, Transform t){
		col.transform.SetParent(t);                        // make controller parent
		col.GetComponent<Rigidbody>().isKinematic = true;  // turn off physics
		controller.TriggerHapticPulse(2000);               // vibrate controller
		Debug.Log("Grabbing object!");
	}

	void ThrowObject(Collider col){
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

	void OnTriggerStay(Collider col){
		if(grabableObjects.Contains(col.gameObject.tag)){
			if(controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
				ReleaseAction(col);
			else if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
				GrabObject(col, transform);
		}
	}
}
