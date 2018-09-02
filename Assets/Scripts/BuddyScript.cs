using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyScript : MonoBehaviour {

	private List<GameObject> guards = new List<GameObject>();
	public Transform guard_c;

	void Start () {
		foreach(Transform guard in guard_c) {
			guards.Add(guard.gameObject);
		}
	}
	
	void OnCollisionEnter(Collision other) {
		Debug.Log("Collided with " + other.gameObject.name);
		if(guards.Contains(other.gameObject)) {
			Debug.Log("Buddy got found :c");
		}
	}

	void Update () {
		// TODO: buddy movement on rails
	}

}
