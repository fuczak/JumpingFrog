using UnityEngine;
using System.Collections;

public class Frog : MonoBehaviour {

	public float moveTime;
	public float turnTime;
	public AnimationCurve moveCurve;
	public AnimationCurve jumpCurve;

	private Vector3 moveDirection;
	private bool shouldMove = false;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldMove) {
			StartCoroutine (Move ());
		}
	}

	public void StartMoving(Vector3 direction) {
		moveDirection = direction;
		shouldMove = true;
	}

	public void ChangeDirection(Vector3 newDirection) {
		float turnAngle = Vector3.Angle (moveDirection, newDirection);
		float turnSign = Vector3.Cross (moveDirection, newDirection).y;

		moveDirection = newDirection;
		LeanTween.rotateAroundLocal (gameObject, Vector3.up, turnAngle * turnSign, turnTime).setEase(LeanTweenType.easeInOutCirc);
	}

	private IEnumerator Move() {
		shouldMove = false;

		float timer = 0.0f;

		Vector3 startPos = transform.position;
		Vector3	endPos = startPos + moveDirection;

		WaitForEndOfFrame wait = new WaitForEndOfFrame ();

		while (timer <= moveTime) {
			Vector3 newPosition = Vector3.Lerp (startPos, endPos, moveCurve.Evaluate (timer / moveTime));
			rb.MovePosition (new Vector3(
				newPosition.x,
				jumpCurve.Evaluate(timer/moveTime),
				newPosition.z
			));

			timer += Time.deltaTime;

			yield return wait;
		}

		rb.MovePosition (endPos);

		shouldMove = true;
	}
}
