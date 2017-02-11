using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {
	public List<Controller> controllers = new List<Controller>();
	public GameObject leftContObj, rightContObj, headContObj;
	public Transform worldObj;

	public float mouseSwipeSensitivity = 100.0f, viveSwipeSensitivity = 1000.0f;
	Vector2 swipeDirection;

	Vector3 currentPos;

	void Awake() {
		if (leftContObj) controllers.Add(new Controller(leftContObj));
		if (rightContObj) controllers.Add(new Controller(rightContObj));
	}

	void Update() {
		GetKeyboardInputs();
		GetViveInputs();
	}
	void LateUpdate() {
		foreach (Controller cont in controllers) {
			cont.lastPosition = cont.trackedObj.transform.position;
		}
	}

	/* if (controller.GetAxis() != Vector2.zero) Debug.Log(gameObject.name + controller.GetAxis());
	if (controller.GetHairTriggerDown()) Debug.Log(gameObject.name + " Trigger Press");
	if (controller.GetHairTriggerUp()) Debug.Log(gameObject.name + " Trigger Release");
	if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) Debug.Log(gameObject.name + " Grip Press");
	if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) Debug.Log(gameObject.name + " Grip Release");
	if (controller.GetPress(SteamVR_Controller.ButtonMask.Grip)) Debug.Log(gameObject.name + " Grip Held");*/
	void GetViveInputs() {
		foreach (Controller cont in controllers) {
			SteamVR_Controller.Device c = cont.controller;

			//if (c.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
			if (c.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
				currentPos = cont.trackedObj.transform.position;
				Vector3 diff = currentPos - cont.lastPosition;

				swipeDirection = new Vector2(diff.x * Time.deltaTime * viveSwipeSensitivity, diff.y * Time.deltaTime * viveSwipeSensitivity);

				worldObj.RotateAround(worldObj.transform.position, headContObj.transform.right, swipeDirection.y);
				worldObj.RotateAround(worldObj.transform.position, headContObj.transform.up, -swipeDirection.x);
			}
		}
	}

	void GetControllerInputs() {
		// TODO Write controller input
	}

	void GetKeyboardInputs() {
		// TODO Write keyboard input

		if (Input.GetKey(KeyCode.Space)) {
			swipeDirection = new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * mouseSwipeSensitivity, Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSwipeSensitivity);
			

			worldObj.RotateAround(worldObj.transform.position, headContObj.transform.right, swipeDirection.y);
			worldObj.RotateAround(worldObj.transform.position, headContObj.transform.up, -swipeDirection.x);
		}
	}
}

[System.Serializable]
public class Controller {
	public SteamVR_TrackedObject trackedObj;
	public SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
	public Vector3 lastPosition;

	public Controller(GameObject contObj) {
		trackedObj = contObj.GetComponent<SteamVR_TrackedObject>();
	}
}
