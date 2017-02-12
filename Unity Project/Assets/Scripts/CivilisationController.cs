using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilisationController : MonoBehaviour {
    public Transform planet;
    public CivilisationValues values;
    GameObject workerPrefab;
    public Color32 civilisationColor;
	public GodType requiredType = GodType.None;

	List<WorkerController> workers = new List<WorkerController>();
    int civIndex;
    GamestateController gamestate;
	GodType lastType = GodType.None;
	float godTypeTimer = 0;
	int godActivatedCounter = 0;

	float noneTypeTimer = 0;
	int nonePause;
	float tickDownTimer = 0;
	int randInt = 0;

    void Start() {
        nonePause = Random.Range(3,15);
    }

	public void Setup(GamestateController gamestate, int civIndex) {
        values.Generate();

        for (int i = 0; i < values.initialWorkerCount; i++) {
            MakeWorker();
        }
    }

	private void Update()
	{

		if (requiredType == GodType.None) {

			noneTypeTimer += Time.deltaTime;
			if (noneTypeTimer >= nonePause) {

				if (workers.Count < values.minWorkerCount)
				{

					randInt = Random.Range(1, 3);

					if (randInt == 1)
					{
						requiredType = GodType.Rain;
					}
					else if (randInt == 2)
					{
						requiredType = GodType.Sun;
					}
					else if (randInt == 3)
					{
						requiredType = GodType.Birth;
					}

				}

				else if (workers.Count > values.maxWorkerCount)
				{

					randInt = Random.Range(1, 3);

					if (randInt == 1)
					{
						requiredType = GodType.Rain;
					}
					else if (randInt == 2)
					{
						requiredType = GodType.Sun;
					}
					else if (randInt == 3)
					{
						requiredType = GodType.Death;
					}

				}

				else
				{

					randInt = Random.Range(1, 2);

					if (randInt == 1)
					{
						requiredType = GodType.Rain;
					}
					else if (randInt == 2)
					{
						requiredType = GodType.Sun;
					}
				}

			}
		}

		if (workers.Count < values.minWorkerCount || workers.Count > values.maxWorkerCount)
		{

			tickDownTimer += Time.deltaTime;
			if (tickDownTimer >= 1)
			{
				tickDownTimer = 0;
				values.statistic -= 0.01f;
			}

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
        if (type != lastType) {
			godTypeTimer = 0;
			godActivatedCounter = 0;
		}

        if (godTypeTimer >= 1.0f) {
			godActivatedCounter++;
			godTypeTimer = 0;
		}

        if (type == requiredType) {
			if (type == GodType.Rain || type == GodType.Sun) {
				if (godActivatedCounter == 1)
				{
					values.statistic += 0.1f;
					noneTypeTimer = 0;
					nonePause = Random.Range(3, 15);
					requiredType = GodType.None;
				}
			}
			else if (type == GodType.Birth) {
				if (godActivatedCounter == 1)
				{
					values.statistic += 0.1f;
					for (int i = 0; i < 5; i++) MakeWorker();
					noneTypeTimer = 0;
					nonePause = Random.Range(3, 15);
					requiredType = GodType.None;
				}
			}
			else if (type == GodType.Death) {
				if (godActivatedCounter == 1)
				{
					values.statistic += 0.1f;
					for (int i = 0; i < 5; i++) KillWorker(i);
					noneTypeTimer = 0;
					nonePause = Random.Range(3, 15);
					requiredType = GodType.None;
				}
			}
		}
		else
		{
			if ((requiredType == GodType.Sun && type == GodType.Rain) || (requiredType == GodType.Rain && type == GodType.Sun))
			{
				if (godActivatedCounter > 1)
				{
					values.statistic -= 0.25f;
					godActivatedCounter = 1;
				}
			}
			else
			{
				if (type == GodType.Rain || type == GodType.Sun)
				{
					if (godActivatedCounter > 1)
					{
						values.statistic -= 0.1f;
						godActivatedCounter = 1;
					}
				}
				else if (type == GodType.Birth)
				{
					if (godActivatedCounter > 1)
					{
						for (int i = 0; i < 5; i++) MakeWorker();
						godActivatedCounter = 1;
					}
				}
				else if (type == GodType.Death)
				{
					if (godActivatedCounter > 1)
					{
						for (int i = 0; i < 5; i++) KillWorker(i);
						godActivatedCounter = 1;
					}
				}
			}
        }

		godTypeTimer += Time.deltaTime;
	}

	public void DoShot(GodType type)
	{

		if (type == GodType.Lightning)
		{
			int amount = Random.Range(2, 6);
			for (int i = 0; i < amount; i++) KillWorker(i);
		}
		else if (type == GodType.Meteor)
		{
			int amount = Random.Range(8, 12);
			for (int i = 0; i < amount; i++) KillWorker(i);
		}
		else if (type == GodType.Grab)
		{
			// TODO Determine which dude
		}
		else if (type == GodType.Squish)
		{
			// TODO Determine which dude
		}

	}
}

[System.Serializable]
public class CivilisationValues {
    public float statistic;
    public int birthRate;
    public int deathRate;
    public int initialWorkerCount;
    public int maxWorkerCount;
	public int minWorkerCount;

	public void Generate() {
        this.statistic = Random.Range(0.15f, 0.35f);
        this.birthRate = Random.Range(1150, 1250);
        this.deathRate = Random.Range(1450, 1550);
        this.initialWorkerCount = Random.Range(10, 25);
        this.maxWorkerCount = this.initialWorkerCount * 2;
		this.minWorkerCount = this.initialWorkerCount / 2;
	}
}
