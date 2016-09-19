using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Field : MonoBehaviour {
	
	private List<WalkableTile> walkableTiles = new List<WalkableTile>();

	void Start () {
		foreach (WalkableTile tile in GetComponentsInChildren<WalkableTile> ()) {
			walkableTiles.Add (tile);
		}
	}

	private void HighlightTiles(bool shouldHighlight) {
		foreach (WalkableTile tile in walkableTiles) {
			if (tile.CanPlaceArrow()) {
				tile.HighlightTile (shouldHighlight);
			}
		}
	}

}
