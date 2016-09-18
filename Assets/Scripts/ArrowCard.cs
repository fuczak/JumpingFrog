using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ArrowCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {


	public GameObject arrowPrefab;
	public LayerMask layerMask;

	private GameObject arrow;
	private RaycastHit hit;

	public void OnBeginDrag(PointerEventData data) {
		arrow = (GameObject) Instantiate (arrowPrefab, data.position, Quaternion.identity);
		arrow.SetActive (true);
	}

	public void OnDrag(PointerEventData data) {
		Ray ray = Camera.main.ScreenPointToRay (data.position);
		Physics.Raycast (ray, out hit, Mathf.Infinity, layerMask);

		if (hit.transform != null && hit.transform.GetComponent<Tile>().CanPlaceArrow()) {
			arrow.transform.position = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);
		}

	}

	public void OnEndDrag(PointerEventData data) {
		if (hit.transform != null && hit.transform != null) {
			hit.transform.GetComponent<Tile> ().PlaceObject ();
			arrow.GetComponent<Arrow> ().StartMoving ();
		}
	}

}
