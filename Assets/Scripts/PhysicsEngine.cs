using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicsEngine : MonoBehaviour {

	public float mass;							// [kg]
	public Vector3 velocity = Vector3.zero;		// [m/s]
	public Vector3 netForce = Vector3.zero;		// N [kg m/s^2]
	public bool showTrails = true;

	private List<Vector3> forceVectors = new List<Vector3>();	// N [kg m/s^2]
	private LineRenderer lineRenderer;

	void Start() {
		InitializeThrustTrails ();
	}

	void FixedUpdate() {
		RenderThrustTrails();
		UpdatePosition();
	}

	public void ApplyForce(Vector3 force) {
		this.forceVectors.Add(force);
//		Debug.Log("Adding force: " + force + " to " + gameObject.name);
	}

	private void InitializeThrustTrails()
	{
		lineRenderer = gameObject.AddComponent<LineRenderer> ();
		lineRenderer.material = new Material (Shader.Find ("Sprites/Default"));
		lineRenderer.startWidth = 0.2f;
		lineRenderer.endWidth = 0.2f;
		lineRenderer.useWorldSpace = false;
		lineRenderer.startColor = Color.yellow;
		lineRenderer.endColor = Color.yellow;
	}

	private void RenderThrustTrails() {
		if (this.showTrails) {
			lineRenderer.enabled = true;
			lineRenderer.numPositions = showTrails ? (this.forceVectors.Count * 2) : 0;

			int i = 0;
			foreach (Vector3 forceVector in this.forceVectors) {
				lineRenderer.SetPosition(i, Vector3.zero);
				lineRenderer.SetPosition(i+1, -forceVector);
				i = i + 2;
			}
		} 
		else {
			lineRenderer.enabled = false;
		}
	}

	private void SumForces() {
		this.netForce = Vector3.zero;

		foreach (Vector3 force in this.forceVectors) {
			this.netForce += force;
		}

		return;
	}

	private void UpdatePosition() {
		Vector3 acceleration = Vector3.zero;
		SumForces();

		this.forceVectors.Clear();

		if (mass > 0) {
			acceleration = this.netForce / this.mass;
		}

		this.velocity += acceleration * Time.deltaTime;
		transform.position += velocity * Time.deltaTime;
	}
}
