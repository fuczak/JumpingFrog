using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Vector3 startDirection;
	public Frog frog;
	public GameObject GuiManager;

	private int score;

	void Start() {
		score = 0;

		GuiManager.SendMessage ("PrepareInitialGui");
	}

	private void StartLevel() {
		frog.StartMoving (startDirection);
	}

	private void AddScore() {
		score += 1;

		GuiManager.SendMessage ("UpdateScore", score);
	}

	private void PlayerReachedFinishBlock() {
		frog.ChangeDirection (Vector3.zero);
		Debug.Log ("Game over");
	}
}
