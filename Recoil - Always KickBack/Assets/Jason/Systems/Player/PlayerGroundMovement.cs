using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the basic left-right on-the-ground movement.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerGroundMovement : MonoBehaviour {

    [SerializeField]
    private float speed = 1;

    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // Get movement axis
        float moveInput = Input.GetAxis("Horizontal");
        float moveSpeed = moveInput * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + new Vector3(moveSpeed, 0));
	}
}
