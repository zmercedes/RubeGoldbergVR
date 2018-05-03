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
}