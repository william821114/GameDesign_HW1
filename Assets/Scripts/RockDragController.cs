using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDragController : MonoBehaviour {

	public float maxStretch = 3.0f;
	public LineRenderer catapultFrontLine;
	public LineRenderer catapultBackLine;
	public AudioSource shootSound;
	public AudioSource hitSound;


	private Transform catapult;
	private SpringJoint2D spring;
	private Rigidbody2D rigidbody2D;
	private bool OnClicked;
	private Ray RayToMouse;
	private Ray LeftCatapultToRock;
	private float maxStretchSqr;
	private float circleRadius;
	private Vector2 preVelocity;
	private CircleCollider2D circle;
	private AudioSource ShootSound;

	void Awake() {
		spring = this.GetComponent<SpringJoint2D> ();
		rigidbody2D = this.GetComponent<Rigidbody2D> ();
		circle = this.GetComponent<CircleCollider2D> ();

		catapult = spring.connectedBody.transform ;
	}


	// Use this for initialization
	void Start () {
		SetupLineRenderer ();
		RayToMouse = new Ray (catapult.position, Vector3.zero);
		LeftCatapultToRock = new Ray (catapultFrontLine.transform.position, Vector3.zero);
		maxStretchSqr = maxStretch * maxStretch;
		circleRadius = circle.radius;
	}
	
	// Update is called once per frame
	void Update () {
		if (OnClicked)
			Dragging ();

		if (spring != null) {
			if (!rigidbody2D.isKinematic && preVelocity.sqrMagnitude > rigidbody2D.velocity.sqrMagnitude) {
				Destroy (spring);
				rigidbody2D.velocity = preVelocity;
			}

			if (!OnClicked)
				preVelocity = rigidbody2D.velocity;

			UpdateLineRenderer ();
		} 
		else {
			catapultFrontLine.enabled = false;
			catapultBackLine.enabled = false;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		hitSound.Play ();
	}

	void SetupLineRenderer (){
		catapultFrontLine.SetPosition (0, catapultFrontLine.transform.position);
		catapultBackLine.SetPosition (0, catapultBackLine.transform.position);

		catapultFrontLine.sortingLayerName = "CatapultFront";
		catapultBackLine.sortingLayerName = "CatapultBack";

		catapultFrontLine.sortingOrder = 3;
		catapultBackLine.sortingOrder = 1;
	}

	void UpdateLineRenderer ()
	{
		Vector2 catapultToRock = this.transform.position - catapultFrontLine.transform.position;
		LeftCatapultToRock.direction = catapultToRock;

		Vector3 holdPoint = LeftCatapultToRock.GetPoint (catapultToRock.magnitude + circleRadius);
		catapultFrontLine.SetPosition (1, holdPoint);
		catapultBackLine.SetPosition (1, holdPoint);
	}

	void OnMouseDown() {
		spring.enabled = false;
		OnClicked = true;
	}

	void OnMouseUp() {
		spring.enabled = true;
		rigidbody2D.isKinematic = false;
		OnClicked = false;
		shootSound.Play ();

		ApplicationData.rockNumber--;
	}

	void Dragging() {
		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - catapult.position;

		if (catapultToMouse.sqrMagnitude > maxStretchSqr) {
			RayToMouse.direction = catapultToMouse;
			mouseWorldPoint = RayToMouse.GetPoint (maxStretch);
		}

		mouseWorldPoint.z = 0f;
		this.transform.position = mouseWorldPoint;
	}

}
