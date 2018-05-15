using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	Vector3 currentPos;
	// Use this for initialization
	void Start () {
		currentPos = transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.parent.position != currentPos){
			float mag = (currentPos - transform.parent.position).magnitude;
			transform.Rotate(Vector3.forward * mag * 50f);
			currentPos = transform.parent.position;
		}
	}
}
