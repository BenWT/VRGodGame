using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerDirectionTrigger : MonoBehaviour {

	// Use this for initialization
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Worker")
		{
			other.GetComponent<WorkerController>().UpdateDirection();
		}
	}
}
