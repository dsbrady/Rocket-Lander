using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour {

	private const float G = 6.67e-11f;				// [m^3 / (kg * s^2)]
	private List<PhysicsEngine> physicsEngines = new List<PhysicsEngine>();

	// Use this for initialization
	void Start () {
		physicsEngines = FindObjectsOfType<PhysicsEngine>().ToList();
	}

	void FixedUpdate () {
		CalculateGravity();
	}

	public void AddGravitationalBody(PhysicsEngine physicsEngine) {
		this.physicsEngines.Add(physicsEngine);
	}

	private void CalculateGravity() {
		float distance = 0f;
		Vector3 distanceUnitVector;
		double gravity = 0;
		Vector3 gravityVector;
		float massA = 0f;
		float massB = 0f;

		foreach (PhysicsEngine physicsEngineA in physicsEngines) {
			foreach (PhysicsEngine physicsEngineB in physicsEngines) {
				if (physicsEngineA != physicsEngineB) {
					massA = physicsEngineA.mass;
					massB = physicsEngineB.mass;
					distance = Vector3.Distance(physicsEngineA.transform.position, physicsEngineB.transform.position);
					gravity = G * massA * massB / Mathf.Pow(distance, 2f);
					distanceUnitVector = (physicsEngineB.transform.position - physicsEngineA.transform.position).normalized;
					gravityVector = distanceUnitVector * (float)gravity;
//					Debug.Log("Calculating force exerted on " + physicsEngineA.name + " due to the gravity of " + physicsEngineB.name);
//					Debug.Log("G: " + G + " massA: " + massA + " massB: " + massB + " distance: " + distance + " gravity: " + gravity + " " + gravityVector);

					physicsEngineA.ApplyForce(gravityVector);
				}
			}
		}
	}
}
