using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class BuddyScript : MonoBehaviour {

    private NavMeshAgent nma;
	private List<GameObject> guards = new List<GameObject>();
    private List<Vector3> patrolPoints = new List<Vector3>();
    private int currentPatrolTargetIndex = 0;
    private int previousPatrolTargetIndex;
    private float patrolLegDistance;

    public float patrolLegPercentage;
    public Transform patrolListParent;
    public Transform guard_c;

    public GameObject EndGameCanvas;
    public GameObject WinGameCanvas;
    public PlayerMovementController pmc;

	void Start () {
        nma = GetComponent<NavMeshAgent>();
        foreach (Transform guard in guard_c) {
			guards.Add(guard.gameObject);
		}
        patrolLegPercentage = Mathf.Clamp(patrolLegPercentage, 0.5f, 0.9f);
        foreach (Transform p in patrolListParent)
        {
            patrolPoints.Add(new Vector3(p.position.x, 0, p.position.z));
        }
        //keep following two lines together
        currentPatrolTargetIndex = patrolPoints.Count - 1;
        setNextPatrolTarget();
    }
	
	void OnCollisionEnter(Collision other) {
		if(guards.Contains(other.gameObject)) {
			Debug.Log("Buddy got found :c");
            ShowEndGameMenu();
		}
	}

    private void ShowEndGameMenu()
    {
        EndGameCanvas.SetActive(true);
        Destroy(pmc);
    }

    void OnTriggerEnter()
    {
        Debug.Log("Buddy made it !!!");
        WinGameCanvas.SetActive(true);
        Destroy(this.gameObject);

    }

    private void setNextPatrolTarget()
    {
        previousPatrolTargetIndex = currentPatrolTargetIndex;
        currentPatrolTargetIndex = (1 + currentPatrolTargetIndex) % patrolPoints.Count;
        nma.destination = patrolPoints[currentPatrolTargetIndex];
        patrolLegDistance = Vector3.Distance(patrolPoints[previousPatrolTargetIndex], patrolPoints[currentPatrolTargetIndex]);
    }

    void Update () {
        Vector3 currentPos = new Vector3(transform.position.x, 0, transform.position.z);
        if (Vector3.Distance(currentPos, patrolPoints[currentPatrolTargetIndex]) < (1 - patrolLegPercentage) * patrolLegDistance)
        {
            setNextPatrolTarget();
        }
    }

}
