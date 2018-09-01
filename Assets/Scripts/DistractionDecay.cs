using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionDecay : MonoBehaviour {

private Transform guard_c;

	public void killPrep(float timeUntilDeath, Transform guards) {
		guard_c = guards;
		Invoke("kill", timeUntilDeath);
	}

	public void kill() {
		// for every child in guards of a certain component type
		foreach( GuardBehaviorController child in guard_c.GetComponentsInChildren<GuardBehaviorController>() )
		{
			child.deaggro(this.gameObject);
		}

		Destroy(this.gameObject);
	}

}
