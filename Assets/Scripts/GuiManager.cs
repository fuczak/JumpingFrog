using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiManager : MonoBehaviour {

	public Text scoreText;
	public RectTransform topPanel;
	public RectTransform arrowPanel;
	public Button startButton;
	public float tweenTime;
	public LeanTweenType easing;

	private ArrowCard[] arrowCards;

	void Start() {
		topPanel.anchoredPosition3D = new Vector3 (0, topPanel.rect.height, 0);
		arrowPanel.anchoredPosition3D = new Vector3(0, topPanel.rect.height * -1, 0);

		arrowCards = GetComponentsInChildren<ArrowCard> ();
	}

	private void PrepareInitialGui(int[] arrowNumbers) {
		LeanTween.move (topPanel, Vector3.zero, tweenTime).setEase (easing);
		LeanTween.move (arrowPanel, Vector3.zero, tweenTime).setEase (easing);

		for (int i = 0; i < 4; i++) {
			arrowCards [i].SetCardNumber (arrowNumbers [i]);
		}
	}

	private void OnStartButtonClick() {
		startButton.interactable = false;

		LeanTween.alphaText (startButton.GetComponentInChildren<Text> ().GetComponent<RectTransform> (), 0, tweenTime)
			.setEase (easing)
			.setOnComplete (() => {
				LeanTween.move(startButton.GetComponent<RectTransform>(), new Vector3(0, topPanel.rect.height, 10), tweenTime)
					.setEase(easing);
		});

		LeanTween.move (Camera.main.gameObject, new Vector3 (-3f, 2f, -3f), tweenTime)
			.setEase (easing)
			.setDelay (tweenTime / 2);
		LeanTween.rotateLocal (Camera.main.gameObject, new Vector3 (30f, 45f, 0), tweenTime)
			.setEase (easing);

		UpdateScore (0);
		scoreText.gameObject.SetActive (true);
	}

	private void UpdateScore(int score) {
		scoreText.text = string.Format ("Flies eaten: {0}/{1}", score, 3);

		LeanTween.colorText (scoreText.GetComponent<RectTransform> (), Color.red, tweenTime / 2)
			.setEase(LeanTweenType.easeSpring)
			.setLoopPingPong(1);
	}
}
