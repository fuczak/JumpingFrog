using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public Vector3 startDirection;
	public GameObject frog;
	public Text scoreText;

	private int score = 0;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			frog.GetComponent<Frog> ().StartMoving (startDirection);
		}
	}

	private void AddScore() {
		score += 1;
		scoreText.text = string.Format ("Score: {0}", score);
		LeanTween.colorText (scoreText.GetComponent<RectTransform> (), Color.red, 0.4f)
			.setEase(LeanTweenType.easeShake)
			.setLoopPingPong(1);
	}
}
