using UnityEngine;

public class Rotator : MonoBehaviour {

	Vector3 currentPos;

	void Start () {
		currentPos = transform.parent.position;
	}
	
	void Update () {
		if(transform.parent.position != currentPos){
			float mag = (currentPos - transform.parent.position).magnitude;
			transform.Rotate(Vector3.forward * mag * 50f);
			currentPos = transform.parent.position;
		}
	}
}
