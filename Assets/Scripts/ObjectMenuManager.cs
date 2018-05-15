/* Zoilo Mercedes
 * Object Menu Manager Manager
 * Activates on right touch pad press, creates objects on trigger press
 */
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour {

	// object manager ui reference
	private Transform OMUI;

	// menu object information
	private GameObject[] objects;
	public GameObject[] objectPrefabs;
	public int[] quantityOfObject;

	// is the manager currently spawning multiple items?
	private bool multiSpawning = false;

	// object currently being looked at
	int currentObject = 0;

	void Awake () {
		OMUI = transform.GetChild(4).GetChild(1);
		objects = new GameObject[4];
		for(int i = 0; i < 4; i++){
			objects[i] = transform.GetChild(i).gameObject;
			OMUI.GetChild(i).GetComponent<Text>().text = quantityOfObject[i] + " left";
		}
	}

	// pages left on menu manager
	public void MenuLeft(){
		if(!multiSpawning){
			objects[currentObject].SetActive(false);
			OMUI.GetChild(currentObject).gameObject.SetActive(false);
			currentObject--;
			currentObject = (currentObject < 0) ? objects.Length - 1 : currentObject;
			objects[currentObject].SetActive(true);
			OMUI.GetChild(currentObject).gameObject.SetActive(true);
		}
	}

	// pages right on menu manager
	public void MenuRight(){
		if(!multiSpawning){
			objects[currentObject].SetActive(false);
			OMUI.GetChild(currentObject).gameObject.SetActive(false);
			currentObject++;
			currentObject = (currentObject > objects.Length - 1) ? 0 : currentObject;
			objects[currentObject].SetActive(true);
			OMUI.GetChild(currentObject).gameObject.SetActive(true);
		}
	}

	public void SpawnObject(){
		if(multiSpawning){
			Instantiate(objectPrefabs[currentObject +1], objects[currentObject].transform.position, objects[currentObject].transform.rotation);
			multiSpawning = false;
			OMUI.GetChild(currentObject+1).gameObject.SetActive(false);
			OMUI.GetChild(currentObject).gameObject.SetActive(true);
			QuantityTextUpdate();
		}
		// check quantity to see if any more can be spawned
		else if(quantityOfObject[currentObject] > 0){
			Instantiate(objectPrefabs[currentObject], objects[currentObject].transform.position, objects[currentObject].transform.rotation);
			
			MultiSpawnToggle();

			if(!multiSpawning)
				QuantityTextUpdate();
		}	
	}

	void QuantityTextUpdate(){
		quantityOfObject[currentObject]--;
		OMUI.GetChild(currentObject).GetComponent<Text>().text = quantityOfObject[currentObject] + " left";
	}

	// checks to see if object has more objects to spawn
	void MultiSpawnToggle(){
		if(currentObject == 3 && !multiSpawning){
			multiSpawning = true;
			OMUI.GetChild(currentObject).gameObject.SetActive(false);
			OMUI.GetChild(currentObject +1).gameObject.SetActive(true);
		}
	}
}