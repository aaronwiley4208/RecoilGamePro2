using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Very basic script that just holds the gun away from the player,
/// based on the mouse position.
/// </summary>
public class GunHolder : MonoBehaviour {

    public Transform gunObj;
    public Transform playerObj;

    private float gunToPlayerDistance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = playerObj.transform.position.z - Camera.main.transform.position.z;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        // Only let it go a certain distance from the player
        Vector3 playerToMouse = transform.position - mousePos;
        gunObj.position = transform.position - (playerToMouse.normalized * gunToPlayerDistance);
    }

    public void SetGunDistance(float distance) {
        gunToPlayerDistance = distance;
    }
}
