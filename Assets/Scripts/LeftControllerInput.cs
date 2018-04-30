/* Zoilo Mercedes
 * Left Controller Input Manager
 */
using UnityEngine;
using System;

public class LeftControllerInput : MonoBehaviour {

	// controller references
	private SteamVR_Controller.Device controller;
	private SteamVR_TrackedObject trackedObj;

	// teleporting
	public GameObject arc;
	private ArcRenderer arcRenderer;
	private LayerMask teleMask;
	private Vector3 teleportLocation;
	private GameObject player;

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		arcRenderer = arc.GetComponent<ArcRenderer>();
		player = transform.parent.gameObject;
	}
	
	void Update () {
		controller = SteamVR_Controller.Input((int)trackedObj.index);

		if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !arc.activeSelf)
			arc.SetActive(true);

		// teleport code
		if(controller.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad) && arc.activeSelf){
			if(arcRenderer.aimerObject.activeSelf){
				player.transform.position = arcRenderer.aimerObject.transform.position;
				print("teleported to " + transform.position);
			}
			arcRenderer.aimerObject.SetActive(false);
			arc.SetActive(false);
		}

		if(controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
			Debug.Log("left touched!");
	}

	public float throwForce = 1.5f;

	void OnTriggerStay(Collider col){
		if(col.gameObject.CompareTag("throwable")){
			if(controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)){ 
				ThrowObject(col);
			} else if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)){
				GrabObject(col);
			}
		}
	}

	void GrabObject(Collider coll){
		coll.transform.SetParent(gameObject.transform);     // make controller parent
		coll.GetComponent<Rigidbody>().isKinematic = true;  // turn off physics
		controller.TriggerHapticPulse(2000);				// vibrate controller
		Debug.Log("Grabbing object!");
	}

	void ThrowObject(Collider coll){
		coll.transform.SetParent(null);
		Rigidbody rigidBody = coll.GetComponent<Rigidbody>();
		rigidBody.isKinematic = false;
		rigidBody.velocity = controller.velocity * throwForce;
		rigidBody.angularVelocity = controller.angularVelocity;
		Debug.Log("Released object!");
	}
}