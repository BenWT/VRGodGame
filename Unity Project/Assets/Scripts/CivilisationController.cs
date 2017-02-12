using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilisationController : MonoBehaviour {
    public Transform planet;
    public CivilisationValues values;
    GameObject workerPrefab;
    public Color32 civilisationColor;
	public GodType requiredType = GodType.Rain;

	List<WorkerController> workers = new List<WorkerController>();
    int civIndex;
    GamestateController gamestate;
	GodType lastType = GodType.Rain;
	float godTypeTimer = 0;
	int godActivatedCounter = 0;

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
		if (workers.Count > 0) workers.RemoveAt(index);
    }

	public void TakeHit(GodType type) {
		if (type != lastType)
		{
			godTypeTimer = 0;
			godActivatedCounter = 0;
		}

		if (godTypeTimer >= 1.0f)
		{
			godActivatedCounter++;
			godTypeTimer = 0;	
		}

		if (type == requiredType)
		{
			if (type == GodType.Rain || type == GodType.Sun)
			{
				if (godActivatedCounter == 1) values.statistic += 0.1f;
				else values.statistic -= 0.1f;
			}
			else if (type == GodType.Birth)
			{
				for (int i = 0; i < 5; i++) MakeWorker();
			}
			else if (type == GodType.Death)
			{
				for (int i = 0; i < 5; i++) KillWorker(i);
			}
		}
		else
		{

		}

		godTypeTimer += Time.deltaTime;
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
