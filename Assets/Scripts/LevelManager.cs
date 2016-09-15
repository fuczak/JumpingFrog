using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public Vector3 startDirection;
	public GameObject frog;

	private int score = 0;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			frog.GetComponent<Frog> ().StartMoving (startDirection);
		}
	}

	private void AddScore() {
		score += 1;
		Debug.Log (score);
	}
}
