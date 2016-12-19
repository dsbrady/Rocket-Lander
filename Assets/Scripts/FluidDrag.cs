using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PhysicsEngine))]
public class FluidDrag : MonoBehaviour {

	public float dragConstant;
	[Range(1f, 2f)]
	public float velocityExponent;

	private PhysicsEngine physicsEngine;

	// Use this for initialization
	void Start () {
		physicsEngine = GetComponent<PhysicsEngine>();
	}
	
	void FixedUpdate () {
		ApplyDrag();	
	}

	private void ApplyDrag() {
		Vector3 velocityVector = this.physicsEngine.velocity;
		float speed = velocityVector.magnitude;
		float dragMagnitude = CalculateDrag(speed);
		Vector3 dragVector = dragMagnitude * -velocityVector.normalized;
//		Debug.Log("DragVector: " + dragVector);
		physicsEngine.ApplyForce(dragVector);
	}

	private float CalculateDrag(float speed) {
		float dragMagnitude = this.dragConstant * Mathf.Pow(speed, this.velocityExponent);

		return dragMagnitude;
	}
}
