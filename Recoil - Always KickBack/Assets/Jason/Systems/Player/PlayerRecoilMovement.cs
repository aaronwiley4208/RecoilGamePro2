using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls basic recoil movement with the gun.
/// Must be attached to the player.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerRecoilMovement : MonoBehaviour {

    [SerializeField]
    private float gravityMultiplier = 1;

    [Header("Gun Properties")]
    [SerializeField]
    [Tooltip("A visual representation of the gun object, moved by player's mouse")]
    private Transform gunObj;
    [SerializeField]
    [Tooltip("How far away the gun should be held from the player")]
    private float gunToPlayerDistance;

    [Header("Recoil Properties")]
    [SerializeField]
    [Tooltip("Force to apply on click")]
    private float recoilForce;

    private Rigidbody rigidbody;
    public int numClicks;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // Calculate where the gun should be based on the mouse position relative to the player.
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        // Only let it go a certain distance from the player
        Vector3 playerToMouse = transform.position - mousePos;
        gunObj.position = transform.position - (playerToMouse.normalized * gunToPlayerDistance);

        // If user clicks, send an impulse force
        if (Input.GetMouseButtonDown(0)) {
            // Calc the direction.
            Vector3 gunToPlayer = (transform.position - gunObj.position).normalized;
            rigidbody.AddForce(gunToPlayer * recoilForce, ForceMode.Impulse);
            numClicks++;
        }
    }

    // Do physics stuff 
    void FixedUpdate() {
        // Apply gravity
        rigidbody.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
    }
}
