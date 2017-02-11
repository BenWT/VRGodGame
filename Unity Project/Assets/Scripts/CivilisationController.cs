using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilisationController : MonoBehaviour {
    public Transform planet;
    public CivilisationValues values;
    GameObject workerPrefab;
    public Color32 civilisationColor;

    List<WorkerController> workers = new List<WorkerController>();
    int civIndex;
    GamestateController gamestate;

    public  void Setup(GamestateController gamestate, int civIndex) {
        values.Generate();

        for (int i = 0; i < values.initialWorkerCount; i++) {
            MakeWorker();
        }
    }

    public void MakeWorker() {
        // TODO Generate Workers
        GameObject newWorker = Instantiate(workerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        newWorker.transform.parent = transform;
        WorkerController workerCont = newWorker.GetComponent<WorkerController>();
        workers.Add(workerCont);
        workerCont.Setup(workers.IndexOf(workerCont));
    }

    public void KillWorker(int index) {
        workers.RemoveAt(index);
    }
}

[System.Serializable]
public class CivilisationValues {
    public float statistic;
    public int birthRate;
    public int deathRate;
    public int initialWorkerCount;
    public int maxWorkerCount;

    public void Generate() {
        this.statistic = Random.Range(0.15f, 0.35f);
        this.birthRate = Random.Range(1150, 1250);
        this.deathRate = Random.Range(1450, 1550);
        this.initialWorkerCount = Random.Range(10, 25);
        this.maxWorkerCount = this.initialWorkerCount * 2;
    }
}
