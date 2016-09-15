using UnityEngine;
using System.Collections;

public class Fly : MonoBehaviour {

	public float rotationSpeed;
	public float radiusSpeed;
	public float radius;
	public float flyHeight;
	public Transform center;

	private GameObject levelManager;

	void Start() {
		levelManager = GameObject.FindGameObjectWithTag ("LevelManager");
	}

	void Update() {
		transform.RotateAround (center.position, Vector3.up, rotationSpeed * Time.deltaTime);
		transform.position = Vector3.MoveTowards (transform.position, GetDesiredPosition(), Time.deltaTime * radiusSpeed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Frog")) {
			Destroy (gameObject);
		}

		levelManager.SendMessage ("AddScore");
	}

	private Vector3 GetDesiredPosition() {
		Vector3 desiredPos =  (transform.position - center.position).normalized * radius + center.position;
		desiredPos = new Vector3(desiredPos.x, desiredPos.y + Random.Range(-flyHeight, flyHeight), desiredPos.z);

		return desiredPos;
	}
}
