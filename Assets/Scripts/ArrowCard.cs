using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {


	public GameObject arrowPrefab;
	public LayerMask layerMask;
	public Field field;
	public GuiManager GuiManager;

	private GameObject arrow;
	private RaycastHit hit;
	private Vector3 startingPos;
	private bool isOnGrid = false;

	private Text cardNumberText;
	private int _cardNumberValue = 0;
	public int CardNumberValue {
		get {
			return _cardNumberValue;
		}
		set {
			_cardNumberValue = value;
			SetCardNumberText (_cardNumberValue);
		}
	}

	void Start() {
		cardNumberText = GetComponentInChildren<Text> ();
	}

	public void SetCardNumber(int value) {
		CardNumberValue = value;
	}

	private void SetCardNumberText(int value) {
		cardNumberText.text = string.Format ("x {0}", value);
	}

	private bool IsDragable() {
		return CardNumberValue > 0;
	}

	public void OnBeginDrag(PointerEventData data) {
		if (!IsDragable ()) {
			return;
		}

		CardNumberValue -= 1;
		startingPos = Camera.main.ScreenToWorldPoint(data.position);
		arrow = (GameObject) Instantiate (arrowPrefab, data.position, Quaternion.identity);
		arrow.GetComponent<Arrow> ().startingPos = startingPos;
		arrow.GetComponent<Arrow> ().parent = GetComponent<ArrowCard> ();
		arrow.SetActive (true);

		field.SendMessage ("HighlightTiles", true);
	}

	public void OnDrag(PointerEventData data) {
		if (arrow == null)
			return;

		Ray ray = Camera.main.ScreenPointToRay (data.position);
		Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask);

		Vector3 worldPos = Camera.main.ScreenToWorldPoint (data.position);

		if (hit.transform != null &&
		    hit.transform.GetComponent<WalkableTile> () != null &&
		    hit.transform.GetComponent<WalkableTile> ().CanPlaceArrow ()) {

			isOnGrid = true;

			LeanTween.move (arrow, new Vector3 (hit.transform.position.x, 0, hit.transform.position.z), 0.3f)
				.setEase (LeanTweenType.easeOutCirc);
		} else if (isOnGrid) {
			isOnGrid = false;

			LeanTween.move (arrow, new Vector3 (worldPos.x, 0, worldPos.z), 0.3f)
				.setEase (LeanTweenType.easeOutCirc);			
		} else {
			LeanTween.cancel (arrow);
			arrow.transform.position = new Vector3 (worldPos.x, 0.5f, worldPos.z);
		}
	}

	public void OnEndDrag(PointerEventData data) {
		if (arrow == null)
			return;

		LeanTween.cancel (arrow);

		if (hit.transform != null
			&& hit.transform.GetComponent<WalkableTile> () != null
			&& hit.transform.GetComponent<WalkableTile>().CanPlaceArrow()) {
			arrow.GetComponent<Arrow> ().PlaceOnTile (hit.transform.GetComponent<WalkableTile> ());
		} else {
			arrow.GetComponent<Arrow> ().ReturnToStartingPosition();
			CardNumberValue += 1;
		}

		arrow = null;

		field.SendMessage ("HighlightTiles", false);
	}

}
