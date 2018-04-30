using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

	public List<GameObject> objectList;
	public List<GameObject> objectPrefabList;

	int currentObject = 0;

	void Awake () {
		foreach(Transform child in transform)
			objectList.Add(child.gameObject);
	}

	public void MenuLeft(){
		objectList[currentObject].SetActive(false);
		currentObject--;
		currentObject = (currentObject < 0) ? objectList.Count - 1 : currentObject;
		objectList[currentObject].SetActive(true);
	}

	public void MenuRight(){
		objectList[currentObject].SetActive(false);
		currentObject++;
		currentObject = (currentObject > objectList.Count - 1) ? 0 : currentObject;
		objectList[currentObject].SetActive(true);
	}

	public void SpawnCurrentObject(){
		Instantiate(objectPrefabList[currentObject], objectList[currentObject].transform.position, objectList[currentObject].transform.rotation);
	}
}