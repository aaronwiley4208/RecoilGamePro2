using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for the pistol, links to a bunch of other scripts and controls them.
/// </summary>
public class Pistol : MonoBehaviour {
    [Header("Links")]
    // Links to the gun barrel, which actually fires the bullets
    public GunBarrel gunBarrel;
    // Link to the recoil movement, which moves the player on firing
    public PlayerRecoilMovement recoilMovement;
    // Link to the thing that holds the gun
    public GunHolder gunHolder;
    // Link to the thing that controls clips and reload.
    public GunClip gunClip;
    // Link to the thing that holds bayonettes for this gun
    public BayonetteHolder bayonetteHolder;
    // Link to the UI for this gun
    public GameObject UI;

    [SerializeField]
    private Transform player;
    [Tooltip("Link to the Air Reloader, just for the upgrade component")]
    public ReloadInAir airReloader;

    [Header("Gun Properties")]
    [SerializeField]
    private GunManagement.Guns gunType;
    [SerializeField]
    [Tooltip("Number of air reloads for this gun. This should always start at 0")]
    private int numAirReloads;
    [SerializeField]
    [Tooltip("How far away the gun is held")]
    private float gunToPlayerDistance;
    [SerializeField]
    private float recoilForce;
    [SerializeField]
    [Tooltip("Starting clip size for this gun")]
    private int clipSize;
    [SerializeField]
    private bool fullAuto;
    [SerializeField]
    [Tooltip("How many shots fire per second")]
    private int fireRate = 1;
    private float fireTimer;

    [Header("Sound Stuff")]
    [SerializeField]
    private AudioSource gunShot;

    // Whether or not the gun is usable. Sets as true when picked up

    public bool active;

	// Use this for initialization
	void Start () {
        gunHolder.SetGunDistance(gunToPlayerDistance);
        gunClip.SetClipSize(clipSize);
        fireTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // Update timer
        fireTimer += Time.deltaTime;
        // Get Direction from player to gun
        Vector3 playerToGun = (player.position - transform.position).normalized;
        if (Input.GetMouseButtonDown(0)) {
            Fire(playerToGun);
        } else if (Input.GetMouseButton(0) && fullAuto) {
            if (fireTimer > (1 / (float)fireRate)) {
                Fire(playerToGun);
                fireTimer = 0;
            }
        }
	}

    private void Fire(Vector3 direction) {
        if (!gunClip.Fire(direction))
            return;
        // If the clip says we can fire, fire bullet and recoil.
        recoilMovement.Recoil(recoilForce, direction);
        gunBarrel.Fire(-direction);
        gunShot.Play();
    }

    /// <summary>
    /// Set the gun to active, turning on its UI and this gameobject, allowing it to shoot.
    /// </summary>
    public void Activate() {
        active = true;
        gameObject.SetActive(true);
        UI.SetActive(true);
    }

    public void UpgradeForce(float forceDelta) {
        recoilForce *= (1 + forceDelta);
    }

    public void UpgradeClip(int clipDelta) {
        clipSize += clipDelta;
        gunClip.SetClipSize(clipSize);
    }

    public void UpgradeClip(float clipDelta) {
        clipSize = ((clipSize * clipDelta) < 1) ? clipSize+1 : clipSize + Mathf.RoundToInt(clipSize * clipDelta);
        gunClip.SetClipSize(clipSize);
    }

    public void UpgradeAirReloads(int reloadDelta) {
        numAirReloads += reloadDelta;
        airReloader.SetMaxReloads(gunType, numAirReloads);
    }

    public void UpgradeBayonette(int bayonetteDelta) {
        for (int i = 0; i < bayonetteDelta; i++)
            bayonetteHolder.AddKnife();
    }

    /// <summary>
    /// Upgrade the scale of the bayonettes.
    /// </summary>
    /// <param name="upgradeScale">The percentage to upgrade by</param>
    public void UpgradeBayonetteScale(float upgradeScale) {
        bayonetteHolder.ScaleKnives(upgradeScale);
    }
}
