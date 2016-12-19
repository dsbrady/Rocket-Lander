using System.Collections;
using UnityEngine;

[RequireComponent (typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {

	public float fuelMass;				// [kg]
	public float maxThrust;				// kN [1000 kg  m / s^2]

	[Range (0, 1f)]
	public float thrustPercent;

	public Vector3 thrustUnitVector;	// [none]

	private float thrust;				// N [kg m / s^2]
	private PhysicsEngine physicsEngine;

	void Start() {
		physicsEngine = GetComponent<PhysicsEngine>();
		physicsEngine.mass += this.fuelMass;
	}

	void Update() {
		// If key pressed is the spacebar, call ExertThrust()
		if (Input.GetKey(KeyCode.Space)) {


			float fuelRequired = GetFuelRequired();

			if (this.fuelMass > 0 && this.fuelMass >= fuelRequired) {
				this.fuelMass -= fuelRequired;
				physicsEngine.mass -= fuelRequired;
				ExertThrust();
			}
			else {
				Debug.LogWarning("You're out of fuel!");
			}
		}
	}

	private void ExertThrust() {
		this.thrust = this.thrustPercent * this.maxThrust * 1000f;

		Vector3 thrustVector = this.thrustUnitVector.normalized * this.thrust;
		Debug.Log("Applying thrust: " + thrustVector);
		physicsEngine.ApplyForce(thrustVector);
	}

	private float GetFuelRequired() {
		float exhaustMassFlow;				// [kg/s]
		float effectiveExhaustVelocity;		// [m/s]

		effectiveExhaustVelocity = 4462f;	// liquid hydrogen engine
		exhaustMassFlow = this.thrust / effectiveExhaustVelocity;

		return exhaustMassFlow * Time.deltaTime;
	}
}
