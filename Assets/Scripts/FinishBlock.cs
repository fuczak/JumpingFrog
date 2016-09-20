using UnityEngine;
using System.Collections;

public class FinishBlock : MonoBehaviour {

	public LevelManager levelManager;

	private AudioSource audio;

	void Start() {
		audio = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Frog")) {
			audio.Play ();
			levelManager.SendMessage ("PlayerReachedFinishBlock");
		}
	}
}
