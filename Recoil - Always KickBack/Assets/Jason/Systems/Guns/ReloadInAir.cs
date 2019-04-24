using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadInAir : MonoBehaviour {

    [Header("Reload Fields")]
    [SerializeField]
    [Tooltip("The time that it takes a gun to reload")]
    private float timeToReload;

    [Header("UI Reload Images")]
    [SerializeField]
    private Image pistolImage;
    [SerializeField]
    private Image shotgunImage;
    [SerializeField]
    private Image machineGunImage;
    [SerializeField]
    private Image sniperImage;

    [Header("Gun Clips")]
    [SerializeField]
    private GunClip pistolClip;
    [SerializeField]
    private GunClip shotgunClip;
    [SerializeField]
    private GunClip machineGunClip;
    [SerializeField]
    private GunClip sniperClip;

    [Header("Reload UI Components")]
    [SerializeField]
    private AirReloadUI pistolReloadIcons;
    [SerializeField]
    private AirReloadUI shotgunReloadIcons;
    [SerializeField]
    private AirReloadUI machineGunReloadIcons;
    [SerializeField]
    private AirReloadUI sniperReloadIcons;

    //[Header("Max Reloads Per Gun")]
    //[SerializeField]
    //private int pistolReloads;
    //[SerializeField]
    //private int shotgunReloads;
    //[SerializeField]
    //private int machineGunReloads;
    //[SerializeField]
    //private int sniperReloads;

    private Dictionary<GunManagement.Guns, Image> gunReloadImages;
    private Dictionary<GunManagement.Guns, GunClip> gunClips;
    private Dictionary<GunManagement.Guns, int> gunMaxReloads;
    private Dictionary<GunManagement.Guns, int> gunCurrentReloads;
    private Dictionary<GunManagement.Guns, AirReloadUI> reloadIcons;

	// Use this for initialization
	void Start () {
        gunReloadImages = new Dictionary<GunManagement.Guns, Image>();
        gunReloadImages.Add(GunManagement.Guns.PISTOL, pistolImage);
        gunReloadImages.Add(GunManagement.Guns.SHOTGUN, shotgunImage);
        gunReloadImages.Add(GunManagement.Guns.MACHINEGUN, machineGunImage);
        gunReloadImages.Add(GunManagement.Guns.SNIPER, sniperImage);
        gunClips = new Dictionary<GunManagement.Guns, GunClip>();
        gunClips.Add(GunManagement.Guns.PISTOL, pistolClip);
        gunClips.Add(GunManagement.Guns.SHOTGUN, shotgunClip);
        gunClips.Add(GunManagement.Guns.MACHINEGUN, machineGunClip);
        gunClips.Add(GunManagement.Guns.SNIPER, sniperClip);
        gunMaxReloads = new Dictionary<GunManagement.Guns, int>();
        gunMaxReloads.Add(GunManagement.Guns.PISTOL, 0);
        gunMaxReloads.Add(GunManagement.Guns.SHOTGUN, 0);
        gunMaxReloads.Add(GunManagement.Guns.MACHINEGUN, 0);
        gunMaxReloads.Add(GunManagement.Guns.SNIPER, 0);
        gunCurrentReloads = new Dictionary<GunManagement.Guns, int>();
        gunCurrentReloads.Add(GunManagement.Guns.PISTOL, 0);
        gunCurrentReloads.Add(GunManagement.Guns.SHOTGUN, 0);
        gunCurrentReloads.Add(GunManagement.Guns.MACHINEGUN, 0);
        gunCurrentReloads.Add(GunManagement.Guns.SNIPER, 0);
        reloadIcons = new Dictionary<GunManagement.Guns, AirReloadUI>();
        reloadIcons.Add(GunManagement.Guns.PISTOL, pistolReloadIcons);
        reloadIcons.Add(GunManagement.Guns.SHOTGUN, shotgunReloadIcons);
        reloadIcons.Add(GunManagement.Guns.MACHINEGUN, machineGunReloadIcons);
        reloadIcons.Add(GunManagement.Guns.SNIPER, sniperReloadIcons);
	}
	
    /// <summary>
    /// Start the reload process for a gun, assuming we have any reloads left for this gun.
    /// </summary>
    /// <param name="gunType"></param>
    public void StartReload(GunManagement.Guns gunType) {
        print("Trying to reload " + gunType);
        // Check if there are reloads left for this gun
        if (gunCurrentReloads[gunType] > 0) {
            print("Reloading " + gunType);
            gunCurrentReloads[gunType]--;
            StartCoroutine("ReloadGun", gunType);
        }
    }

    IEnumerator ReloadGun(GunManagement.Guns gun) {
        float currentTime = 0;
        // Wait for time, filling up the reload feedback image
        while (currentTime < timeToReload) {
            currentTime += Time.deltaTime;
            gunReloadImages[gun].fillAmount = currentTime / timeToReload;
            yield return null;
        }
        // Actuall reload
        gunClips[gun].Reload();
        gunReloadImages[gun].fillAmount = 0;
        reloadIcons[gun].UseOneReload(gunCurrentReloads[gun]);
    }

    /// <summary>
    /// Stop all pending reloads on all guns
    /// </summary>
    public void Reset() {
        foreach (GunManagement.Guns gun in gunReloadImages.Keys) {
            gunReloadImages[gun].fillAmount = 0;
            gunCurrentReloads[gun] = gunMaxReloads[gun];
            reloadIcons[gun].ResetUI();
        }
        StopAllCoroutines();
    }

    public void SetMaxReloads(GunManagement.Guns gunType, int numReloads) {
        gunMaxReloads[gunType] = numReloads;
        gunCurrentReloads[gunType] = numReloads;
        reloadIcons[gunType].SetReloads(numReloads);
    }
}
