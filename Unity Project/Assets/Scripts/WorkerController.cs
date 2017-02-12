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

	public void Setup(int index) {
        this.index = index;
        timer += Random.Range(0.0f, 1.0f);

    }

    void Update() {
		// TODO Move around constraints of civ

		/*	Random rnd = new Random();
			int direction = rnd.Next(1, 5);

			Vector3 newPosition = new Vector3(-1, 0, 0) * (moveSpeed * Time.deltaTime);
			newPosition = transform.position + newPosition;
			newPosition.x = Mathf.Clamp(newPosition.x, -101, 126);
			transform.position = newPosition;*/

		if (planetGravity)
		{
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
