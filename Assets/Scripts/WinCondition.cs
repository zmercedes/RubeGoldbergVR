/* Zoilo Mercedes
 * Win Condition Level loader
 * Hitting target after having collected collectibles will call WinCon
 */
using UnityEngine;

public class WinCondition : MonoBehaviour {

	public GameObject winUI;
	SteamVR_LoadLevel levelLoader;

	void Awake () {
		levelLoader = GetComponent<SteamVR_LoadLevel>();
		FindObjectOfType<BallActions>().winCon += WinCon;
	}

	void WinCon(){
		winUI.SetActive(true);
	}

	void LoadNextLevel(){
		levelLoader.Trigger();
	}
}