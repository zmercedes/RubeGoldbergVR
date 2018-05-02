/* Zoilo Mercedes
 * Left Controller Input Manager
 */
using UnityEngine;
using System;
using System.Linq;

public class LeftControllerInput : ControllerInput, MonoBehaviour {

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
	
	void Update (){
		controller = SteamVR_Controller.Input((int)trackedObj.index);

		if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !arc.activeSelf)
			arc.SetActive(true);

		if(controller.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad) && arc.activeSelf)
			Teleport();

		if(controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
			Debug.Log("left touched!");
	}

	// teleport code
	void Teleport(){
		if(arcRenderer.aimerObject.activeSelf){
			player.transform.position = arcRenderer.aimerObject.transform.position;
			print("teleported to " + transform.position);
		}
		arcRenderer.aimerObject.SetActive(false);
		arc.SetActive(false);
	}

	void OnTriggerStay(Collider col){
		if(grabableObjects.Contains(col.gameObject.tag)){
			if(controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
				ReleaseActionPicker(col, controller, col.gameObject.tag);
			else if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
				GrabObject(col, controller, transform);
		}
	}
}
