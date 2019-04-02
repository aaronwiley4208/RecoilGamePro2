using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the pistol, links to a bunch of other scripts and controls them.
/// </summary>
public class Pistol : MonoBehaviour {

    // Links to the gun barrel, which actually fires the bullets
    public GunBarrel gunBarrel;
    // Link to the recoil movement, which moves the player on firing
    public PlayerRecoilMovement recoilMovement;
    // Link to the thing that holds the gun
    public GunHolder gunHolder;
    // Link to the thing that controls clips and reload.
    public GunClip gunClip;

    [SerializeField]
    private Transform player;

    [Header("Gun Properties")]
    [SerializeField]
    [Tooltip("How far away the gun is held")]
    private float gunToPlayerDistance;
    [SerializeField]
    private float recoilForce;
    [SerializeField]
    [Tooltip("Starting clip size for this gun")]
    private int clipSize;

	// Use this for initialization
	void Start () {
        gunHolder.SetGunDistance(gunToPlayerDistance);
        gunClip.SetClipSize(clipSize);
	}
	
	// Update is called once per frame
	void Update () {
        // Get Direction from player to gun
        Vector3 playerToGun = (player.position - transform.position).normalized;
        if (Input.GetMouseButtonDown(0)) {
            Fire(playerToGun);
        }
	}

    private void Fire(Vector3 direction) {
        if (!gunClip.Fire())
            return;
        // If the clip says we can fire, fire bullet and recoil.
        recoilMovement.Recoil(recoilForce, direction);
    }
}
