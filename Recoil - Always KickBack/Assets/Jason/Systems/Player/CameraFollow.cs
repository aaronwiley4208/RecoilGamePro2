using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public enum FollowMode { STICKY, DRAG, SMOOTH }

    [SerializeField]
    private Transform player;
    [SerializeField]
    private FollowMode mode;

    [SerializeField]
    [Tooltip("Offset from the player, probably best to just set a vertical one")]
    private Vector3 offset;

    [Header("Camera Drag Settings")]
    [SerializeField]
    [Tooltip("The speed at which the camera will follow the player")]
    private float cameraSpeed;

    [Header("Smooth Damp Settings")]
    [SerializeField]
    [Tooltip("The current speed of the camera")]
    private Vector3 velocity;
    [SerializeField]
    [Tooltip("The time it should take to reach the target")]
    private float smoothTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Move the camera's x and y towards the player based on mode
        Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;
        switch (mode) {
            case FollowMode.STICKY:
                transform.position = targetPos;
                break;
            case FollowMode.DRAG:
                // Just move towards the target pos at our designated speed
                Vector3 cameraToPlayer = targetPos - transform.position;
                // Don't overshoot the position movement
                if (cameraToPlayer.magnitude < cameraSpeed * Time.deltaTime)
                    transform.position = targetPos;
                else 
                    transform.position += (cameraToPlayer * cameraSpeed * Time.deltaTime);
                break;
            case FollowMode.SMOOTH:
                transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
                break;
        }
	}
}
