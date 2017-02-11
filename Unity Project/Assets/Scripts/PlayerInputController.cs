using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {

	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device controller {
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Update() {
		if (!trackedObj) trackedObj = GetComponent<SteamVR_TrackedObject>();

		GetViveInputs();
	}

	void GetViveInputs() {

		if (controller.GetAxis() != Vector2.zero)
		{
		    Debug.Log(gameObject.name + controller.GetAxis());
		}

		if (controller.GetHairTriggerDown())
		{
		    Debug.Log(gameObject.name + " Trigger Press");
		}

		if (controller.GetHairTriggerUp())
		{
		    Debug.Log(gameObject.name + " Trigger Release");
		}

		if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
		{
		    Debug.Log(gameObject.name + " Grip Press");
		}

		if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
		{
		    Debug.Log(gameObject.name + " Grip Release");
		}
	}

	void GetControllerInputs() {
		// TODO Write controller input
	}

	void GetKeyboardInputs() {
		// TODO Write keyboard input
	}
}
