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
		// when parent != null, ball is being grabbed
		isGrabbed = transform.parent != null;
		
		if(isGrabbed)
			CheatCheck();
	}

	void CheatCheck(){
		RaycastHit hit;
		int mask = 1 << 8;

		// if collider below is not "platform", isCheating 
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 15f, mask))
			cheating = hit.collider.tag != "platform";
	}

	void Reset(){
		transform.position = startPosition;
		cheating = false; 
	}

	void OnCollisionEnter(Collision col){
		if(cheating)
			Reset();

	}
}
