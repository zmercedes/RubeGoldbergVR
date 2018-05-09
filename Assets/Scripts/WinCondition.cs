using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour {


	void Awake () {
		FindObjectOfType<BallActions>().winCon += WinCon;
	}

	void WinCon(){
		// do stuff
	}
}