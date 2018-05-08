/* Zoilo Mercedes
 * Ball Actions
 * Checks for cheating, collides with collectibles
 */
using UnityEngine;

public class BallActions : MonoBehaviour {

	Vector3 startPosition;
	bool cheating = false;
	bool isGrabbed = false;

	void Awake () {
		startPosition = transform.position;
	}
	
	void Update () {
		if(isGrabbed)
			CheatCheck();
	}

	void CheatCheck(){
		if(isGrabbed){
			RaycastHit hit;
			if(Physics.Raycast(transform.position, Vector3.down, out hit, 15f)){
				cheating = hit.collider.tag != "platform";
			}
		}
	}

	void Reset(){
		transform.position = startPosition;
	}

	void OnCollisionEnter(Collision col){
		if(cheating)
			Reset();

	}
}
