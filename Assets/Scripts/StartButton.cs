using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

	private float width;
	private float offset;

	private RectTransform rt;
	private Button btn;

	void Start() {
		rt = GetComponent<RectTransform> ();
		btn = GetComponent<Button> ();
		width = rt.rect.width;
		offset = rt.position.x;
	}

	private void HideButton() {
		btn.interactable = false;

		LeanTween.move (rt, new Vector3 ((width + offset) * -1, 0, 0), 0.5f)
			.setEase (LeanTweenType.easeInCubic)
			.setOnComplete (() => {
				gameObject.SetActive(false);
			});
	}

}