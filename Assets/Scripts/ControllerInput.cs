using UnityEngine;
using System;
using System.Linq;

public class ControllerInput : MonoBehaviour {

	// controller references
	private SteamVR_Controller.Device controller;
	private SteamVR_TrackedObject trackedObj;
	
	// actions
	public event Action<Collider, SteamVR_Controller.Device, Transform> GrabAction;
	public event Action<Collider, SteamVR_Controller.Device, string> ReleaseAction;
	
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
