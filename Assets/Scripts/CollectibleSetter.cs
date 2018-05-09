/* Zoilo Mercedes
 * Collectibles Status
 */
using UnityEngine;

public class CollectibleSetter : MonoBehaviour {

	public GameObject collectible;
	Vector3[] positions;
	int total;

	void Awake () {
		total = transform.childCount;
		FindObjectOfType<BallActions>().reset += Reset;
		positions = new Vector3[total];
		for(int i = 0; i < total; i++){
			positions[i] = transform.GetChild(i).position;
		}
	}

	void Reset(){
		if(transform.childCount < total){
			foreach(Transform child in transform)
				Destroy(child.gameObject);

			foreach(Vector3 position in positions)
				Instantiate(collectible, position, Quaternion.identity, transform);
		}
	}
	
}