using UnityEngine;
using System.Collections;

public class FinishBlock : MonoBehaviour {

	public LevelManager levelManager;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Frog")) {
			levelManager.SendMessage ("PlayerReachedFinishBlock");
		}
	}
}
