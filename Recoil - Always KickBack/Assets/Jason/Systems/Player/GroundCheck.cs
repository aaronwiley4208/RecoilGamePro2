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
    [SerializeField]
    private int numFramesStored;
    
    private Vector3 lastGroundedPosition;
    public List<Vector3> lastGroundedPositions;

	
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
        // If we're still grounded after this check, set the last grounded position
        if (isGrounded) {
            lastGroundedPosition = transform.position;
            lastGroundedPositions.Add(lastGroundedPosition);
            if (lastGroundedPositions.Count > numFramesStored)
                lastGroundedPositions.RemoveAt(0);
        }
	}

    public Vector3 GetLastGroundedPosition() {
        return lastGroundedPosition;
    }
    public Vector3 GetRecentGroundedPosition() {
        return lastGroundedPositions[lastGroundedPositions.Count - 1];
    }
}
