﻿/* Zoilo Mercedes
 * Win Condition Level loader
 * Hitting target after having collected collectibles will call WinCon
 */
using UnityEngine;

public class UI_Manager : MonoBehaviour {

	// controller references...
	public ControllerInput left, right;

	// UI points
	public Transform uInterface;
	private GameObject mainUI;
	private GameObject winUI;

	// win sound
	private AudioSource win;
	private int current = 1;

	void Start() {
		win = GetComponent<AudioSource>();
		mainUI = uInterface.GetChild(1).gameObject;
		winUI = uInterface.GetChild(2).gameObject;
		FindObjectOfType<BallActions>().winCon += OnWin;
		left.UItoggle += ToggleUI;
		right.UItoggle += ToggleUI;
	}

	void ToggleUI(){
		uInterface.gameObject.SetActive(!uInterface.gameObject.activeSelf);
	}

	void OnWin(){
		mainUI.SetActive(false);
		win.Play();
		uInterface.gameObject.SetActive(true);
		winUI.SetActive(true);
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