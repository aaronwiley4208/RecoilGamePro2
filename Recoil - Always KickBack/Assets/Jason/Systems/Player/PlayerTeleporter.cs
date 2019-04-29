using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Teleports the player to their last seen position when they touch teleporter goo
/// </summary>
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerTeleporter : MonoBehaviour {

    [SerializeField]
    [Tooltip("How long to not let the player move.")]
    private float pauseTime;

    private GroundCheck groundCheck;
    private Rigidbody rigidbody;
    private PlayerGroundMovement groundMovement;

	// Use this for initialization
	void Start () {
        groundCheck = GetComponent<GroundCheck>();
        rigidbody = GetComponent<Rigidbody>();
        groundMovement = GetComponent<PlayerGroundMovement>();
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Teleporter") {
            print("Teleport to " + groundCheck.GetRecentGroundedPosition());
            rigidbody.MovePosition(groundCheck.GetRecentGroundedPosition() + new Vector3(0, .01f, 0));
            transform.position = groundCheck.GetRecentGroundedPosition() + new Vector3(0, 0.01f, 0);
            groundCheck.ResetPosHistory();
            StartCoroutine(PausePlayer());
        }
    }

    IEnumerator PausePlayer() {
        groundMovement.enabled = false;
        yield return new WaitForSeconds(pauseTime);
        groundMovement.enabled = true;
    }
}
