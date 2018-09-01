using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class SoundCreationController : MonoBehaviour {
	public GameObject distraction;
	public GameObject distraction_c;

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
			Destroy(Instantiate(distraction, transform.position, distraction.transform.rotation, distraction_c.transform), distractionDeathTime);
		}
		else {
			// handle the cooldown
			timeSinceLastSpawn += Time.deltaTime;
			if(timeSinceLastSpawn >= cooldown) {
				ableToSpawn = true;
			}
		}
	}
}
