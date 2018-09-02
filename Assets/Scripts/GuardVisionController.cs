﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GuardBehaviorController))]
public class GuardVisionController : MonoBehaviour
{
    public float visionConeAngle = 15;
    public float maxVisionDistance = 15;
    public LayerMask mask;// make sure all things with colliders in the active list are on a layer specified in the LayerMask
    public GameObject activeListParent;// a parent of gameobjects to keep track of vision on
    private GuardBehaviorController gb;
    private List<Transform> activeList = new List<Transform>();

    // Use this for initialization
    void Start()
    {
        gb = transform.GetComponent<GuardBehaviorController>();
        foreach(Transform t in activeListParent.transform)
        {
            activeList.Add(t);
        }
    }

    public bool CheckLOS(Transform other)
    {
        Vector3 coneOrigin = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 hereToOther = other.position - coneOrigin;
        //within vision cone
        //Debug.DrawRay(coneOrigin, transform.forward, Color.magenta);
        if(hereToOther.magnitude <= maxVisionDistance && Vector3.Angle(hereToOther, transform.forward) <= visionConeAngle)
        {
            //do a raycast to see if the vision towards target is unobstructed
            RaycastHit hit; 
            if (Physics.Raycast(coneOrigin, hereToOther, out hit, maxVisionDistance, mask, QueryTriggerInteraction.UseGlobal))
            {
                Debug.Log("Raycast hit: " + (hit.collider.name));
                return (hit.collider.name == "Buddy");
            }
        }
        
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in activeList)
        {
            if (CheckLOS(t))
            {
                gb.aggro(t.gameObject, false);
            }
        }
    }
}
