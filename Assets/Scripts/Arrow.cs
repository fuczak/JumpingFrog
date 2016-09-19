using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public Vector3 directionChange;
	public float tweenTime;
	public float tweenDistance;
	public GameObject model;

	private BoxCollider coll;

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

	public void StartMoving() {

		coll.enabled = true;

		LeanTween.move (model, model.transform.position + (directionChange * tweenDistance), tweenTime)
			.setEase (LeanTweenType.easeInOutCubic)
			.setLoopPingPong (-1);
	}
}
