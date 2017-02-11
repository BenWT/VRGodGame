using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour {
    public CivilisationController civilisation;
    int index;
    float timer;
    Vector3 gravityDirection;

    public void Setup(int index) {
        this.index = index;
        timer += Random.Range(0.0f, 1.0f);
    }

    void Update() {
        // TODO Move around constraints of civ
        gravityDirection = (transform.position - civilisation.planet.position).normalized;
        transform.LookAt(civilisation.planet, transform.up);

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
