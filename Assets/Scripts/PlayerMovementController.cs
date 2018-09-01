using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundCreationController))]
public class PlayerMovementController : MonoBehaviour {
    public float speed = 1.0f;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = transform.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = (input * speed * Time.deltaTime);
	}
}
