/* Zoilo Mercedes
 * Right Controller Input Manager
 */
using UnityEngine;

public class RightControllerInput : ControllerInput {

	// object menu
	GameObject objectMenu;
	private ObjectMenuManager omm;

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		objectMenu = transform.GetChild(1).gameObject;
		omm = objectMenu.GetComponent<ObjectMenuManager>();
	}

	void Update () {
		controller = SteamVR_Controller.Input((int)trackedObj.index);
		
		UISelector();

		if(controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
			Toggler();
		
		// right hand functions
		if(controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
			Debug.Log("right touched!");
		
		// activate/deactivate objectMenu on touchpad press
		if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)){
			Debug.Log("right tp pressed down!");
			objectMenu.SetActive(!objectMenu.activeSelf);
		}

		if(objectMenu.activeSelf){
			if(controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad)){
				float touchCurrent = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
				if(touchCurrent < 0f)
					omm.MenuLeft();
				else
					omm.MenuRight();
			}

			if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
				omm.SpawnObject();
		}
	}
}