using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple script that reloads the gun whenever grounded.
/// Though checking just grounding seems to be insufficient, so also doing velocity check.
/// </summary>
public class ReloadOnGround : MonoBehaviour {
    [SerializeField]
    private GroundCheck groundCheck;
    [SerializeField]
    private Rigidbody playerRigidbody;
    [SerializeField]
    private GunClip[] gunClips;

    private float lastVelocity; // Vel of the last frame 
	
	// If player is grounded and moving down, reload
	void OnCollisionEnter (Collision collision) {
        if (collision.collider.tag == "Ground" && collision.relativeVelocity.y > 0) {
            foreach (GunClip clip in gunClips)
                clip.Reload();
        }
	}
}
