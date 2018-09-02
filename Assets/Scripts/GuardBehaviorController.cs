using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GuardBehaviorController : MonoBehaviour {
    private enum State{normal, alerted};
    private State state = State.normal;
    private GameObject aggroTarget = null;
    private NavMeshAgent nma;
    private List<Vector3> patrolPoints = new List<Vector3>();
    private int currentPatrolTargetIndex = 0;
    private int previousPatrolTargetIndex;
    private float patrolLegDistance;

    public float patrolLegPercentage;
    public Transform patrolListParent;

	// Use this for initialization
	void Start () {
        nma = GetComponent<NavMeshAgent>();
        patrolLegPercentage = Mathf.Clamp(patrolLegPercentage, 0.5f, 0.9f);
        foreach(Transform p in patrolListParent)
        {
            patrolPoints.Add(p.position);
        }
        //keep following two lines together
        currentPatrolTargetIndex = patrolPoints.Count - 1;
        setNextPatrolTarget();
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

    // calculates the next patrolpoint to target based on what is closest when this fxn is called
    private void continuePatrol()
    {
        Vector3 currentPos = new Vector3(transform.position.x, 0, transform.position.z);
        int nearestPatrolPointIndex = 0;
        for(int i = 1; i < patrolPoints.Count; ++i)
        {
            if(Vector3.Distance(patrolPoints[i], currentPos)  < Vector3.Distance(patrolPoints[nearestPatrolPointIndex], currentPos))
            {
                nearestPatrolPointIndex = i;
            }
        }
        if (nearestPatrolPointIndex == 0)
        {
            currentPatrolTargetIndex = patrolPoints.Count - 1;
        }
        else
        {
            currentPatrolTargetIndex = nearestPatrolPointIndex - 1;
        }
        setNextPatrolTarget();
    }

    private void setNextPatrolTarget()
    {
        previousPatrolTargetIndex = currentPatrolTargetIndex;
        currentPatrolTargetIndex = (1 + currentPatrolTargetIndex) % patrolPoints.Count;
        nma.destination = patrolPoints[currentPatrolTargetIndex];
        patrolLegDistance = Vector3.Distance(patrolPoints[previousPatrolTargetIndex], patrolPoints[currentPatrolTargetIndex]);
    }

	// Update is called once per frame
	void Update () {
		if(state == State.normal)
        {
            Vector3 currentPos = new Vector3(transform.position.x, 0, transform.position.z);
            // actually patrol things
            if (Vector3.Distance(currentPos, patrolPoints[currentPatrolTargetIndex]) < (1 - patrolLegPercentage) * patrolLegDistance)
            {
                setNextPatrolTarget();
            }
        }else if(state == State.alerted)
        {
            nma.destination = aggroTarget.transform.position;
        }
	}
}
