using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public bool canReceiveObject;
	private bool isEmpty = true;

	public bool CanPlaceArrow() {
		return isEmpty && canReceiveObject;
	}

	public void PlaceObject() {
		isEmpty = false;
	}

	public void RemoveObject() {
		isEmpty = true;
	}

}
