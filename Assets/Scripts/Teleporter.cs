using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

	GameObject destination;
	
	void OnCollisionEnter(Collision col){
		destination = GameObject.Find("Ball_TP_D_Solid(Clone)");
		col.transform.position = destination.transform.GetChild(0).position;
	}	
}
