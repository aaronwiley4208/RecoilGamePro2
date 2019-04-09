using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checks raycasts from two grounding points to see if the player is grounded.
/// </summary>
public class GroundCheck : MonoBehaviour {
    public bool isGrounded;

    [SerializeField]
    private Transform[] groundCheckers;
    [SerializeField]
    private float checkDistance;
    [SerializeField]
    private string groundTag = "Ground";

	
	// Do the check 
	void Update () {
        isGrounded = false;
        foreach (Transform check in groundCheckers) {
            // Shoot a ray down from each, check for ground intersection
            RaycastHit hit;
            Debug.DrawRay(check.position, Vector3.down * checkDistance);
            if (Physics.Raycast(check.position, Vector3.down, out hit, checkDistance)) {
                if (hit.collider.tag == groundTag)
                    isGrounded = true;
            }
        }
	}
}
