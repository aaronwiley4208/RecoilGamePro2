using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Teleports the player to their last seen position when they touch teleporter goo
/// </summary>
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerTeleporter : MonoBehaviour {

    private GroundCheck groundCheck;
    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        groundCheck = GetComponent<GroundCheck>();
        rigidbody = GetComponent<Rigidbody>();
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Teleporter") {
            print("Teleport to " + groundCheck.GetRecentGroundedPosition());
            rigidbody.MovePosition(groundCheck.GetRecentGroundedPosition() + new Vector3(0, .01f, 0));
            transform.position = groundCheck.GetRecentGroundedPosition() + new Vector3(0, 0.01f, 0);
        }
    }
}
