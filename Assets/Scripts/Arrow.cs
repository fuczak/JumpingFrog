﻿using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public Vector3 directionChange;
	public float tweenTime;
	public float tweenDistance;
	public GameObject model;

	private BoxCollider coll;
	private WalkableTile occupiedTile;

	[HideInInspector]
	public Vector3 startingPos;

	void Start() {
		coll = GetComponent<BoxCollider> ();
		coll.enabled = false;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Frog")) {
			other.GetComponent<Frog> ().ChangeDirection (directionChange);
			gameObject.SetActive (false);
		}
	}

	void OnMouseDown() {
		ReturnToStartingPosition ();
	}

	public void StartMoving() {

		coll.enabled = true;
		Debug.Log (model.transform.position);
		LeanTween.move (model, model.transform.position + (directionChange * tweenDistance), tweenTime)
			.setEase (LeanTweenType.easeInOutCubic)
			.setLoopPingPong (-1);
	}

	public void PlaceOnTile(WalkableTile tile) {
		occupiedTile = tile;
		Debug.Log (tile.transform.position);
		transform.position = new Vector3 (occupiedTile.transform.position.x, 0, occupiedTile.transform.position.z);
		tile.PlaceObject ();
		StartMoving ();
	}

	public void ReturnToStartingPosition() {
		coll.enabled = false;

		if (occupiedTile != null) {
			occupiedTile.RemoveObject ();
		}

		LeanTween.cancel (model);

		LeanTween.move (model, startingPos, 0.3f)
			.setEase (LeanTweenType.easeOutCubic)
			.setOnComplete (() => {
				DestroyObject (gameObject);
			});
	}
}
