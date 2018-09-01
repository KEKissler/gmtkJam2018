using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(GuardVisionController))]
[RequireComponent(typeof(NavMeshAgent))]
public class GuardBehaviorController : MonoBehaviour {
    private enum State{normal, alerted};
    private State state = State.normal;
    private GameObject aggroTarget = null;
    private GuardVisionController vc;
    private NavMeshAgent nma;
	// Use this for initialization
	void Start () {
        vc = GetComponent<GuardVisionController>();
        nma = GetComponent<NavMeshAgent>();
	}
	
    public void aggro(GameObject source, bool isSound)
    {
        if(isSound || aggroTarget == null)
        {
            aggroTarget = source;
            state = State.alerted;
            nma.destination = source.transform.position;
        }
    }

    public void deaggro(GameObject source)
    {
        if (source && source == aggroTarget)
        {
            aggroTarget = null;
            state = State.normal;
            continuePatrol();
        }
    }

    //calculates the next patrolpoint to target based on what is closest when this fxn is called
    private void continuePatrol()
    {

    }

	// Update is called once per frame
	void Update () {
		if(state == State.normal)
        {
            
            //patrol things
        }
	}
}
