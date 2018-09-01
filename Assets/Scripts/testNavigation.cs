using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testNavigation : MonoBehaviour {
    private NavMeshAgent nma;
    public GameObject objToFollow;
	// Use this for initialization
	void Start () {
        nma = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        nma.destination = objToFollow.transform.position;
	}
}
