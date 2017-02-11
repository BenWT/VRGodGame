using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {
	public List<Controller> controllers = new List<Controller>();
	public GameObject leftContObj, rightContObj, headContObj;
	public Transform worldObj;

	public float mouseSwipeSensitivity = 100.0f, viveSwipeSensitivity = 100.0f;


	Vector3 swipeDirection;

	void Awake() {
		if (leftContObj) controllers.Add(new Controller(leftContObj));
		if (rightContObj) controllers.Add(new Controller(rightContObj));
	}

	void Update() {
		GetViveInputs();
		//GetKeyboardInputs();
	}
	void LateUpdate() {
		foreach (Controller cont in controllers) {
			cont.Sync();
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

			if (c.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
				swipeDirection = cont.GetPosition() - cont.lastPosition;
				swipeDirection *= Time.deltaTime * viveSwipeSensitivity;
				worldObj.eulerAngles += new Vector3(swipeDirection.y, -swipeDirection.x, 0);
			}
		}
	}

	void GetControllerInputs() {
		// TODO Write controller input
	}

	void GetKeyboardInputs() {
		// TODO Write keyboard input

		if (Input.GetKey(KeyCode.Space)) {
			swipeDirection = new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * mouseSwipeSensitivity, Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSwipeSensitivity, 0);
			worldObj.eulerAngles += new Vector3(swipeDirection.y, -swipeDirection.x, 0);
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

	public void Sync() {
		lastPosition = trackedObj.transform.localPosition;
	}
	public Vector3 GetPosition() {
		return trackedObj.transform.localPosition;
	}
}
