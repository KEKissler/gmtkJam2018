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
    public float aggroRadius = 10.0f;

    private bool ableToSpawn = true;
	private float timeSinceLastSpawn = 0.0f;

    void Start()
    {
        //lr = GetComponent<LineRenderer>();
    }

	// Update is called once per frame
	void Update () {
		if(ableToSpawn && Input.GetAxisRaw("Fire1") >= 0.5) {
			ableToSpawn = false;
			timeSinceLastSpawn = 0.0f;
			
			// Instantiate a Distraction under DistractionContainer
			justInstantiated = Instantiate(distraction, transform.position, distraction.transform.rotation, distraction_c.transform);
			justInstantiated.GetComponent<DistractionDecay>().killPrep(distractionDeathTime, guard_c.transform);	// set up the distraction to die
            Vector3 currentPosition = new Vector3(transform.position.x, 0, transform.position.z);
            // aggro all the guards (for now!!!!!)
            justInstantiated.GetComponent<LineRenderer>().SetPositions(new Vector3[] {
                    ((currentPosition + new Vector3(0,0.5f,0)) + aggroRadius * Vector3.forward),
                    ((currentPosition + new Vector3(0,0.5f,0)) + aggroRadius * (Vector3.forward + Vector3.right).normalized),
                    ((currentPosition + new Vector3(0,0.5f,0)) + aggroRadius * Vector3.right),
                    ((currentPosition + new Vector3(0,0.5f,0)) + aggroRadius * (Vector3.right + Vector3.back).normalized),
                    ((currentPosition + new Vector3(0,0.5f,0)) + aggroRadius * Vector3.back),
                    ((currentPosition + new Vector3(0,0.5f,0)) + aggroRadius * (Vector3.back + Vector3.left).normalized),
                    ((currentPosition + new Vector3(0,0.5f,0)) + aggroRadius * Vector3.left),
                    ((currentPosition + new Vector3(0,0.5f,0)) + aggroRadius * (Vector3.left + Vector3.forward).normalized),
                    ((currentPosition + new Vector3(0,0.5f,0)) + aggroRadius * Vector3.forward)
                    });
            foreach (Transform child in guard_c.transform)
			{
                Vector3 guardPos = new Vector3(child.position.x, 0, child.position.z);

                if (Vector3.Distance(guardPos, currentPosition) <= aggroRadius)
                {
                    
                    child.GetComponent<GuardBehaviorController>().aggro(justInstantiated, true);
                }
			}
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
