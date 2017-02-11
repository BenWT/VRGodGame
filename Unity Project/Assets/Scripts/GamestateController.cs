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
}
