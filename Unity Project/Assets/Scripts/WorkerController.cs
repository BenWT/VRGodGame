using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour {
	public CivilisationController civilisation;
	public int moveSpeed, gravities;
	public float radius = 0.5f;
	public bool planetGravity = true;
	int index;
    float timer;
	Vector3 gravityDirection;
	Vector2 direction;

	void Start()
	{
		direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
	}

	public void Setup(int index) {
        this.index = index;
        timer += Random.Range(0.0f, 1.0f);
	}

	public void UpdateDirection()
	{
		direction = -direction;
		direction += new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
	}

    void Update() {

		if (planetGravity)
		{
			transform.position += transform.up * Time.deltaTime * direction.x;
			transform.position += transform.right * Time.deltaTime * direction.y;

			gravityDirection = (transform.position - civilisation.planet.position).normalized;
			transform.LookAt(civilisation.planet, transform.up);

			Vector3 difference = transform.position - civilisation.planet.position;
			float mag = difference.magnitude;
			transform.position = civilisation.planet.position + Vector3.ClampMagnitude(difference, Mathf.Lerp(mag, radius, Time.deltaTime * gravities)); //reverse pythagoras
		}
		else
		{
			// retain tragetory 
		}
				

        if (timer >= 1) {
            //if (Random.Range(1, civilisation.values.birthRate) == civilisation.values.birthRate) GiveBirth();
            //if (Random.Range(1, civilisation.values.deathRate) == civilisation.values.deathRate) Die();
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    void GiveBirth() {
        civilisation.MakeWorker();
    }

    void Die() {
        civilisation.KillWorker(index);
        Destroy(gameObject);
    }
}
