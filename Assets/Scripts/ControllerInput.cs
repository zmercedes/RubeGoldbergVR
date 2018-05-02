using UnityEngine;
using System;
using System.Linq;

public class ControllerInput : MonoBehaviour {

	// controller references
	private SteamVR_Controller.Device controller;
	private SteamVR_TrackedObject trackedObj;
	
	string[] grabableObjects = {"place","throw"};

	void OnTriggerStay(Collider col){
		if(grabableObjects.Contains(col.gameObject.tag)){
			if(controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
				ReleaseAction(col, controller, col.gameObject.tag);
			else if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
				GrabAction(col, controller, transform);
		}
	}
}
