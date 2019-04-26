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

    [Header("Sound Stuff")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;

    private float lastVelocity; // Vel of the last frame 
    private bool lastGround;

    void Update() {
        // Check if last was false and now is true, basically an OnGroundEnter scenario.
        if (!lastGround && groundCheck.isGrounded) {
            foreach (GunClip clip in gunClips)
                clip.Reload();
            GetComponent<ReloadInAir>().Reset();
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        lastGround = groundCheck.isGrounded;
    }

    // If player is grounded and moving down, reload
    //void OnCollisionEnter (Collision collision) {
    //       if (collision.collider.tag == "Ground" && collision.relativeVelocity.y > 0) {
    //           foreach (GunClip clip in gunClips)
    //               clip.Reload();
    //       }
    //}
}
