/* Zoilo Mercedes
 * Win Condition Level loader
 * Hitting target after having collected collectibles will call WinCon
 */
using UnityEngine;

public class UI_Manager : MonoBehaviour {

	// UI points
	public Transform uInterface;
	GameObject mainUI;
	GameObject winUI;
	int current = 1;

	void Awake () {
		mainUI = uInterface.GetChild(1).gameObject;
		winUI = uInterface.GetChild(2).gameObject;
		FindObjectOfType<BallActions>().winCon += OnWin;
		FindObjectOfType<RightControllerInput>().UItoggle += ToggleUI;
		FindObjectOfType<LeftControllerInput>().UItoggle += ToggleUI;
	}

	void OnWin(){
		mainUI.SetActive(false);
		winUI.SetActive(true);
	}

	void ToggleUI(){
		uInterface.gameObject.SetActive(!uInterface.gameObject.activeSelf);
	}

	public void SetMain(int which){
		mainUI.transform.GetChild(current).gameObject.SetActive(false);
		current = which;
		mainUI.transform.GetChild(current).gameObject.SetActive(true);
	}

	public void LoadNextLevel(){
		GetComponent<SteamVR_LoadLevel>().Trigger();
	}
}