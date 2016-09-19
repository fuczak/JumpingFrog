using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ArrowCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {


	public GameObject arrowPrefab;
	public LayerMask layerMask;
	public Field field;

	private GameObject arrow;
	private RaycastHit hit;

	public void OnBeginDrag(PointerEventData data) {
		arrow = (GameObject) Instantiate (arrowPrefab, data.position, Quaternion.identity);
		arrow.SetActive (true);
		field.SendMessage ("HighlightTiles", true);
	}

	public void OnDrag(PointerEventData data) {
		Ray ray = Camera.main.ScreenPointToRay (data.position);
		Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask);

//		arrow.transform.position = data.worldPosition;

		if (hit.transform != null &&
			hit.transform.GetComponent<WalkableTile>() != null &&
			hit.transform.GetComponent<WalkableTile>().CanPlaceArrow()) {
			Debug.Log (hit.transform.position);
			arrow.transform.position = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);
		}
	}

	public void OnEndDrag(PointerEventData data) {
		field.SendMessage ("HighlightTiles", false);
		if (hit.transform != null && hit.transform != null) {
			hit.transform.GetComponent<WalkableTile> ().PlaceObject ();
			arrow.GetComponent<Arrow> ().StartMoving ();
		}
	}

}
