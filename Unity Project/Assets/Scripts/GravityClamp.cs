using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GravityClamp : MonoBehaviour {

	public Transform planet;
	Vector3 gravityDirection;

	void Update () {
		gravityDirection = (transform.position - planet.position).normalized;
		transform.LookAt(planet, transform.up);
	}
}
