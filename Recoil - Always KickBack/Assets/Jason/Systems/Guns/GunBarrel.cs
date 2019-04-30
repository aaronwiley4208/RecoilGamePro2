using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The barrel of the gun is what shoots the bullets.
/// </summary>
public class GunBarrel : MonoBehaviour {
    public GameObject bulletPrefab;
    public float bulletForce;
    [Tooltip("The angle of the spread, centered at the clicking direction.")]
    [Range(0, 90)]
    public float spreadAngle;
    [Tooltip("The number of bullets in the spread.")]
    public int bulletsPerClick;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private Pistol gun;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Get direction TODO: Link this to the direction from the recoil script
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector3 mouseToPlayer = (mousePos - transform.position).normalized;

        // Check input, if click, fire a bullet
        //if (Input.GetMouseButtonDown(0)) {
        //    Fire(mouseToPlayer);
        //}

        VisualizeSpread(mouseToPlayer);
	}

    public void Fire(Vector3 direction) {
        // check if we should fire a bullet. Don't want to if we're just aiming into a wall
        RaycastHit hit;
        Ray ray = new Ray(player.position, (transform.position - player.position).normalized);
        if (Physics.Raycast(ray, out hit)) {
            if (hit.distance < 1) // too close to wall
                return;
        }

        if (bulletsPerClick == 1) {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(direction));
            Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
            bulletRB.AddForce(direction * bulletForce, ForceMode.Impulse);
        } else {
            // We need to determine the spread if there are multiple bullets
            Vector3[] directions = ComputeSpread(direction);

            for (int i = 0; i < bulletsPerClick; i++) {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(directions[i]));
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
                bulletRB.AddForce(directions[i] * bulletForce, ForceMode.Impulse);
            }
        }
    }

    /// <summary>
    /// Compute the directions for a bullet spread pattern.
    /// The angle will be spread around the initial direction, 
    /// then revolve through the resulting wedge to find other directions.
    /// </summary>
    /// <param name="startDirection">The initial (mouse click) direction</param>
    /// <returns>A series of directions</returns>
    private Vector3[] ComputeSpread(Vector3 startDirection) {
        Vector3[] directions = new Vector3[bulletsPerClick];
        // The interval between bullet directions
        float spreadIncrements = spreadAngle / (bulletsPerClick-1);
        // The first direction, we'll spread from here
        Vector3 initialDir = Quaternion.AngleAxis(spreadAngle / 2, Vector3.forward) * startDirection;
        // Go through all the bullets, creating a dir for them all
        for (int i = 0; i < bulletsPerClick; i++) {
            directions[i] = Quaternion.AngleAxis(-spreadIncrements * i, Vector3.forward) * initialDir;
        }
        return directions;
    }

    private void VisualizeSpread(Vector3 startDirection) {
        Debug.DrawRay(transform.position, startDirection, Color.green);

        float halfSpread = spreadAngle / 2;

        Vector3 topVec = Quaternion.AngleAxis(halfSpread, Vector3.forward) * startDirection;
        Vector3 bottomVec = Quaternion.AngleAxis(-halfSpread, Vector3.forward) * startDirection;
        //Vector3 topVec = new Vector3(startDirection.x, startDirection.magnitude * Mathf.Cos(halfSpread), startDirection.magnitude * Mathf.Sin(halfSpread));
        Debug.DrawRay(transform.position, topVec, Color.red);
        Debug.DrawRay(transform.position, bottomVec, Color.blue);

        float spreadIncrements = spreadAngle / (bulletsPerClick-1);
        for (int i = 0; i < bulletsPerClick; i++) {
            Debug.DrawRay(transform.position, Quaternion.AngleAxis(-spreadIncrements * i, Vector3.forward) * topVec);
        }
    }
}
