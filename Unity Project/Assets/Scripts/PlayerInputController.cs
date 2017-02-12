using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour {
	public List<Controller> controllers = new List<Controller>();
	public GameObject leftContObj, rightContObj, headContObj;
	public Transform worldObj;

	public float mouseSwipeSensitivity = 100.0f, viveSwipeSensitivity = 1000.0f;
	Vector3 swipeDirection;

	void Awake() {
		if (leftContObj) controllers.Add(new Controller(leftContObj));
		if (rightContObj) controllers.Add(new Controller(rightContObj));
	}

	void Update() {
		//GetKeyboardInputs();
		GetViveInputs();
	}
	void LateUpdate() {
		foreach (Controller cont in controllers) {
			cont.lastPosition = cont.trackedObj.transform.position;
		}
	}

	/* if (c.GetAxis() != Vector2.zero) Debug.Log(gameObject.name + controller.GetAxis());
	if (c.GetHairTriggerDown()) Debug.Log(gameObject.name + " Trigger Press");
	if (c.GetHairTrigger()) Debug.Log(gameObject.name + " Trigger Press");
	if (c.GetHairTriggerUp()) Debug.Log(gameObject.name + " Trigger Release");
	if (c.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) Debug.Log(gameObject.name + " Grip Press");
	if (c.GetPress(SteamVR_Controller.ButtonMask.Grip)) Debug.Log(gameObject.name + " Grip Hold");
	if (c.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) Debug.Log(gameObject.name + " Grip Release"); */
	void GetViveInputs() {
		foreach (Controller cont in controllers) {
			SteamVR_Controller.Device c = cont.controller;

			if (c.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
				if (cont.inMenu) cont.inMenu = false;
				else cont.inMenu = true;
			}

			if (cont.inMenu) {
				Debug.Log("In Menu");
			}

			if (c.GetPress(SteamVR_Controller.ButtonMask.Grip)) {
				if (c.GetAxis() != Vector2.zero) {
					worldObj.RotateAround(worldObj.position, cont.trackedObj.transform.right, c.GetAxis().y * viveSwipeSensitivity * Time.deltaTime);
					worldObj.RotateAround(worldObj.position, cont.trackedObj.transform.up, -c.GetAxis().x * viveSwipeSensitivity * Time.deltaTime);
				}
			}

			if (c.GetHairTrigger())
			{
				RaycastHit hit;

				if (Physics.Raycast(cont.trackedObj.transform.position, cont.trackedObj.transform.forward, out hit)) {
					if (hit.collider.tag == "Civilisation")
					{
						CivilisationController civ = hit.collider.GetComponent<CivilisationController>();
						civ.TakeHit(cont.type);
					}
				}
			}

			if (c.GetHairTriggerDown()) {
				RaycastHit hit;

				if (Physics.Raycast(cont.trackedObj.transform.position, cont.trackedObj.transform.forward, out hit)) {
					if (hit.collider.tag == "Civilisation")
					{
						CivilisationController civ = hit.collider.GetComponent<CivilisationController>();
						civ.DoShot(cont.type);
					}
				}
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
	public GodType type = GodType.Grab;
	public bool inMenu = false;

	public Controller(GameObject contObj) {
		trackedObj = contObj.GetComponent<SteamVR_TrackedObject>();
	}
}

[System.Serializable]
public enum GodType
{
	None,
	Rain,
	Sun,
	Birth,
	Death,
	Lightning,
	Meteor,
	Grab,
	Squish
}
