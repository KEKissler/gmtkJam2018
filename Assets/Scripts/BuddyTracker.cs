using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyTracker : MonoBehaviour {
    public GameObject arrowAnchor;
    public GameObject buddy;
	
	// Update is called once per frame
	void Update () {
        Vector3 usToBuddy = buddy.transform.position - transform.position;
        Quaternion newRot = Quaternion.Euler(new Vector3(90f, 0, 270 - Mathf.Rad2Deg * Mathf.Atan2(-usToBuddy.z, usToBuddy.x)));
        arrowAnchor.transform.rotation = newRot;
	}
}
