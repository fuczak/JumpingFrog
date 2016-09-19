﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ArrowCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {


	public GameObject arrowPrefab;
	public LayerMask layerMask;
	public Field field;

	private GameObject arrow;
	private RaycastHit hit;
	private Vector3 startingPos;

	public void OnBeginDrag(PointerEventData data) {
		startingPos = Camera.main.ScreenToWorldPoint(data.position);
		arrow = (GameObject) Instantiate (arrowPrefab, data.position, Quaternion.identity);
		arrow.GetComponent<Arrow> ().startingPos = startingPos;
		arrow.SetActive (true);

		field.SendMessage ("HighlightTiles", true);
	}

	public void OnDrag(PointerEventData data) {
		Ray ray = Camera.main.ScreenPointToRay (data.position);
		Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask);

		Vector3 worldPos = Camera.main.ScreenToWorldPoint (data.position);

		if (hit.transform != null &&
		    hit.transform.GetComponent<WalkableTile> () != null &&
		    hit.transform.GetComponent<WalkableTile> ().CanPlaceArrow ()) {

			LeanTween.move (arrow, new Vector3 (hit.transform.position.x, 0, hit.transform.position.z), 0.3f)
				.setEase (LeanTweenType.easeOutCirc);
		} else {
			arrow.transform.position = new Vector3 (worldPos.x, 0.5f, worldPos.z);
		}
	}

	public void OnEndDrag(PointerEventData data) {
		LeanTween.cancel (arrow);

		if (hit.transform != null && hit.transform.GetComponent<WalkableTile> () != null) {
			arrow.GetComponent<Arrow> ().PlaceOnTile (hit.transform.GetComponent<WalkableTile> ());
		} else {
			arrow.GetComponent<Arrow> ().ReturnToStartingPosition();
		}

		field.SendMessage ("HighlightTiles", false);
	}

}
