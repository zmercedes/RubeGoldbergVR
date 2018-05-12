using UnityEngine;

public class Teleporter : MonoBehaviour {

	GameObject destination;
	
	void OnCollisionEnter(Collision col){
		destination = GameObject.Find("Ball_TP_D_Solid(Clone)");
		Rigidbody ball = col.gameObject.GetComponent<Rigidbody>();
		ball.velocity = Vector3.zero;
		ball.angularVelocity = Vector3.zero;
		col.transform.position = destination.transform.GetChild(0).position;
	}	
}
