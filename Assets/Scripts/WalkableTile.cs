using UnityEngine;
using System.Collections;

public class WalkableTile : MonoBehaviour {

	private bool canReceiveObject = true;
	public bool isEmpty = true;

	private MeshRenderer highlight;

	void Start() {
		highlight = transform.Find ("Highlight").GetComponent<MeshRenderer> ();
	}

	public void HighlightTile(bool shouldHighlight) {
		highlight.enabled = shouldHighlight && CanPlaceArrow();

	}
		
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
