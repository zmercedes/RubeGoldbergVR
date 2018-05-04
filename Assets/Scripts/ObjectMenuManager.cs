using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

	private GameObject[] objects;
	public GameObject[] objectPrefabs;
	public int[] quantityOfObject;

	int currentObject = 0;

	void Awake () {
		objects = new GameObject[transform.childCount];
		for(int i = 0; i < transform.childCount; i++)
			objects[i] = transform.GetChild(i).gameObject;
	}

	public void MenuLeft(){
		objects[currentObject].SetActive(false);
		currentObject--;
		currentObject = (currentObject < 0) ? objects.Length - 1 : currentObject;
		objects[currentObject].SetActive(true);
	}

	public void MenuRight(){
		objects[currentObject].SetActive(false);
		currentObject++;
		currentObject = (currentObject > objects.Length - 1) ? 0 : currentObject;
		objects[currentObject].SetActive(true);
	}

	public void SpawnCurrentObject(){
		// check quantity to see if any more can be spawned
		if(quantityOfObject[currentObject] > 0){
			Instantiate(objectPrefabs[currentObject], objects[currentObject].transform.position, objects[currentObject].transform.rotation);
			quantityOfObject[currentObject]--;
		}	
	}

	public void SpawnTeleporters(){
		// teleporters will be spawned using this code
	}
}