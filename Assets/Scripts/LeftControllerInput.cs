/* Zoilo Mercedes
 * Left Controller Input Manager
 */
using UnityEngine;

public class LeftControllerInput : ControllerInput {

	// teleporting
	public GameObject arc;
	private ArcRenderer arcRenderer;
	private GameObject player;

	void Awake () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		arcRenderer = arc.GetComponent<ArcRenderer>();
		player = transform.parent.gameObject;
	}
	
	void Update (){
		controller = SteamVR_Controller.Input((int)trackedObj.index);

		UISelector();

		if(controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)){
			Toggler();
		}

		if(controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !arc.activeSelf)
			arc.SetActive(true);

		if(controller.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad) && arc.activeSelf)
			Teleport();

		if(controller.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
			Debug.Log("left touched!");
	}

	// teleport code
	void Teleport(){
		if(arcRenderer.targetObject.activeSelf){
			player.transform.position = arcRenderer.targetObject.transform.position;
			print("teleported to " + transform.position);
		}
		arcRenderer.targetObject.SetActive(false);
		arc.SetActive(false);
	}
}