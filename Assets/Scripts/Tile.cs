using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	private GameObject gameObj;
	private bool isEmpty = true;

	public bool CanPlaceArrow() {
		return isEmpty;
	}

	public void PlaceObject(GameObject GO) {
		gameObj = GO;
		isEmpty = false;
	}

	public void RemoveObject() {
		gameObj = null;
		isEmpty = true;
	}

}
