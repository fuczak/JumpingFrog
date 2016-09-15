using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public Vector3 directionChange;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Frog")) {
			other.GetComponent<Frog> ().ChangeDirection (directionChange);
			gameObject.SetActive (false);
		}
	}
}
