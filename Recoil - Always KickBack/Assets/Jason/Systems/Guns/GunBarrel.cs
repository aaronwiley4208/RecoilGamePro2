using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The barrel of the gun is what shoots the bullets.
/// </summary>
public class GunBarrel : MonoBehaviour {
    public GameObject bulletPrefab;
    public float bulletForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Check input, if click, fire a bullet
        if (Input.GetMouseButtonDown(0)) {
            // Get direction TODO: Link this to the direction from the recoil script
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = transform.position.z - Camera.main.transform.position.z;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            Vector3 mouseToPlayer = (mousePos - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(mouseToPlayer));
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(mouseToPlayer * bulletForce, ForceMode.Impulse);
        }
	}
}
