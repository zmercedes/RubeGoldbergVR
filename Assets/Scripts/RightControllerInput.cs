/* Zoilo Mercedes
 * Right Controller Input Manager
 */
using UnityEngine;

public class RightControllerInput : ControllerInput {

	// object menu
	GameObject objectMenu;
	private ObjectMenuManager omm;
	private float touchLast;
	private float touchCurrent;
	private float distance;
	public float swipeSpeed = 50;

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		objectMenu = transform.GetChild(1).gameObject;
		omm = objectMenu.GetComponent<ObjectMenuManager>();
	}

	void Update () {
		controller = SteamVR_Controller.Input((int)trackedObj.index);

		// right hand functions
		if(controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
			Debug.Log("right touched!");

		// setting initial touch position for swiping
		if(controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
			touchLast = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
		
		// activate/deactivate objectMenu on touchpad press
		if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)){
			Debug.Log("right tp pressed down!");
			objectMenu.SetActive(true);
		}

		if(objectMenu.activeSelf){

			if(controller.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad)){
				touchCurrent = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
				if(touchCurrent < 0f)
					omm.MenuLeft();
				else
					omm.MenuRight();
			}
			

			if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
				omm.SpawnCurrentObject();
		}

		// if objectMenu is active and player is touching touchpad,
		// rotate the objectMenu based on swiping left/right on touchpad.
		// if(controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad)){

		// 	distance = touchCurrent - touchLast;
		// 	touchLast = touchCurrent;

		// 	objectMenu.transform.Rotate(Vector3.forward * distance * swipeSpeed);
		// }
	}
}