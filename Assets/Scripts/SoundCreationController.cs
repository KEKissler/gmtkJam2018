using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class SoundCreationController : MonoBehaviour {
	public GameObject distraction;
	public GameObject distraction_c;		// parent object of all distractions (sounds)
	public GameObject guard_c;				// parent object of all guards

	private GameObject justInstantiated;

	public float cooldown = 2.0f;
	public float distractionDeathTime = 2.0f;

	private bool ableToSpawn = true;
	private float timeSinceLastSpawn = 0.0f;

	// Update is called once per frame
	void Update () {
		if(ableToSpawn && Input.GetAxisRaw("Fire1") >= 0.5) {
			ableToSpawn = false;
			timeSinceLastSpawn = 0.0f;
			// Instantiate a Distraction under DistractionContainer
			justInstantiated = Instantiate(distraction, transform.position, distraction.transform.rotation, distraction_c.transform);
			Invoke("deaggro", distractionDeathTime);	// after a bit, the distraction should tell the guards to de-aggro and then delete itself
		}
		else {
			// handle the cooldown
			timeSinceLastSpawn += Time.deltaTime;
			if(timeSinceLastSpawn >= cooldown) {
				ableToSpawn = true;
			}
		}
	}

	public void deaggro() {
		// tell a guard to stop being so mad
		foreach (Transform child in guard_c.transform)
		{
			// call *the guard's* deaggro function
			child.GetComponent<GuardBehaviorController>().deaggro(justInstantiated);
		}
		Destroy(justInstantiated);
	}
}
