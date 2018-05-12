/* Zoilo Mercedes
 * Object Menu Manager Manager
 * Activates on right touch pad press, creates objects on trigger press
 */
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

	private GameObject[] objects;
	public GameObject[] objectPrefabs;
	public int[] quantityOfObject;

	bool multiSpawning = false;

	int currentObject = 0;

	void Awake () {
		objects = new GameObject[transform.childCount];
		for(int i = 0; i < transform.childCount; i++)
			objects[i] = transform.GetChild(i).gameObject;
	}

	// pages left on menu manager
	public void MenuLeft(){
		if(!multiSpawning){
			objects[currentObject].SetActive(false);
			currentObject--;
			currentObject = (currentObject < 0) ? objects.Length - 1 : currentObject;
			objects[currentObject].SetActive(true);
		}
	}

	// pages right on menu manager
	public void MenuRight(){
		if(!multiSpawning){
			objects[currentObject].SetActive(false);
			currentObject++;
			currentObject = (currentObject > objects.Length - 1) ? 0 : currentObject;
			objects[currentObject].SetActive(true);
		}
	}

	public void SpawnObject(){
		if(multiSpawning){
			Instantiate(objectPrefabs[currentObject +1], transform.position, objects[currentObject].transform.rotation);
			multiSpawning = false;
			quantityOfObject[currentObject]--;
		}
		// check quantity to see if any more can be spawned
		else if(quantityOfObject[currentObject] > 0){
			Instantiate(objectPrefabs[currentObject], transform.position, objects[currentObject].transform.rotation);
			
			MultiSpawnToggle();

			if(!multiSpawning)
				quantityOfObject[currentObject]--;
		}	
	}

	// checks to see if object has more objects to spawn
	void MultiSpawnToggle(){
		if(currentObject == 3 && !multiSpawning)
			multiSpawning = true;
	}
}