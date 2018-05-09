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
		positions = new Vector3[total];
		for(int i = 0; i < total; i++){
			positions[i] = transform.GetChild(i).position;
		}
	}

	public bool Collected(){
		return transform.childCount == 0;
	}

	public void Reset(){
		if(transform.childCount < total){
			foreach(Transform child in transform)
				Destroy(child.gameObject);

			foreach(Vector3 position in positions)
				Instantiate(collectible, position, Quaternion.identity, transform);
		}
	}
}