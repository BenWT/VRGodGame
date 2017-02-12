using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateController : MonoBehaviour {
    public List<CivilisationController> civilisations = new List<CivilisationController>();
    public float tickDownAmount;

    void Start() {
        for (int i = 0; i < civilisations.Count; i++) {
            civilisations[i].Setup(this, i);
        }
    }

	private void Update()
	{

		if (civilisations.Count >= 4)
		{
			foreach (CivilisationController c in civilisations)
			{
				if (c.values.statistic >= 1.0f)
				{

					Application.Quit();  //You won! :D

				}
			}
		}
		else
		{

			Application.Quit();   //You lost! D:

		}
	}

	public bool isLowest(CivilisationController civ)
	{
		float lowest = 2.0f;
		float nextLowest = 2.0f;

		foreach (CivilisationController c in civilisations)
		{
			if (c.values.statistic < lowest)
			{
				nextLowest = lowest;
				lowest = c.values.statistic;
			}
		}

		if (civ.values.statistic == lowest && (nextLowest - lowest) >= 0.4f) return true;
		else return false;
	}
	public int getHighest(CivilisationController civ)
	{
		float highest = -2.0f;
		int index = -1;

		foreach (CivilisationController c in civilisations)
		{
			if (c.values.statistic > highest)
			{
				highest = c.values.statistic;
				index = civilisations.IndexOf(c);
			}
		}

		return index;
	}
}
