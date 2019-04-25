using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The hook part of the grappling system. Shot by the grappling gun.
/// Stops and freezes when it collides with an object so the player can swing from it.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class GrapplingHook : MonoBehaviour {

    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
