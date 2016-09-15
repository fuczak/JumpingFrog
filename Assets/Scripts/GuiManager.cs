using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

	public Text scoreText;
	public RectTransform topPanel;
	public RectTransform bottomPanel;
	public Button startButton;
	public float tweenTime;
	public LeanTweenType easing;

	void Start() {
		topPanel.anchoredPosition3D = new Vector3 (0, topPanel.rect.height, 0);
		bottomPanel.anchoredPosition3D = new Vector3(0, topPanel.rect.height * -1, 0);
	}

	private void PrepareInitialGui() {
		LeanTween.move (topPanel, Vector3.zero, tweenTime).setEase (easing);
		LeanTween.move (bottomPanel, Vector3.zero, tweenTime).setEase (easing);
	}

	private void HideStartButton() {
		startButton.interactable = false;

		LeanTween.alphaText (startButton.GetComponentInChildren<Text> ().GetComponent<RectTransform> (), 0, tweenTime)
			.setEase (easing)
			.setOnComplete (() => {
				LeanTween.move(startButton.GetComponent<RectTransform>(), new Vector3(0, topPanel.rect.height, 10), tweenTime)
					.setEase(easing);
		});

		scoreText.gameObject.SetActive (true);
	}

	private void UpdateScore(int score) {
		scoreText.text = string.Format ("Score: {0}", score);

		LeanTween.colorText (scoreText.GetComponent<RectTransform> (), Color.red, 0.4f)
			.setEase(LeanTweenType.easeShake)
			.setLoopPingPong(1);
	}

}
