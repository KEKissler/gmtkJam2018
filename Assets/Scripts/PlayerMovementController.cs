using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundCreationController))]
public class PlayerMovementController : MonoBehaviour {
    public float speed = 1.0f;
    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = transform.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        rb.velocity = (input * speed * Time.deltaTime);
	}
}
