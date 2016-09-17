using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public Vector3 directionChange;
	public float tweenTime;
	public float tweenDistance;
	public GameObject model;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Frog")) {
			other.GetComponent<Frog> ().ChangeDirection (directionChange);
			gameObject.SetActive (false);
		}
	}

	public void StartMoving() {
		LeanTween.move (model, model.transform.position + (directionChange * tweenDistance), tweenTime)
			.setEase (LeanTweenType.easeInOutCubic)
			.setLoopPingPong (-1);
	}
}
