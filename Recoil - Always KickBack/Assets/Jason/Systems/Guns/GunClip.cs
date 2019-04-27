using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunClip : MonoBehaviour {

    public enum GunUIStyles { IMAGEFILL, BULLETS }

    [Header("Clip Info")]
    [SerializeField]
    [Tooltip("The max clip size for this gun. Set by the overall gun script (Pistol for now).")]
    private int clipSize;
    [SerializeField]
    private int clipCount;

    [Header("Reload Fields")]
    [SerializeField]
    private GroundCheck groundCheck;
    [SerializeField]
    private ReloadInAir airReloader;
    [SerializeField]
    private Transform player;

    [Header("UI")]
    [SerializeField]
    private GunUIStyles UIStyle;
    [SerializeField]
    [Tooltip("The UI image must have a fill amount.")]
    private Image clipUI;
    [SerializeField]
    Animation reloadPulseUI;

    [Header("Bullet UI")]
    [SerializeField]
    [Tooltip("The Transform that tells where the bullet icons are gonna go.")]
    private RectTransform BulletUIParent;
    [SerializeField]
    [Tooltip("Where the bullets should stop and form a new column.")]
    private RectTransform BulletUITop;
    [SerializeField]
    [Tooltip("The Prefab for the bullet icons that fill the 'clip'")]
    private GameObject bulletUIPrefab;
    [Tooltip("How vertically far apart each bullet icon should be")]
    [SerializeField]
    private float bulletUISeparation;
    [SerializeField]
    [Tooltip("How far apart each column of bullets should be")]
    private float bulletUIHorSeparation;

    // A collection of all the bullet icons
    public List<GameObject> bullets = new List<GameObject>();

	// Use this for initialization
	void Start () {
        Reload();
	}
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// Checks against the current clip count, tells the caller
    /// if this gun can fire. Starts an air reload if possible
    /// </summary>
    /// <returns>Whether or not the gun can fire.</returns>
    public bool Fire(Vector3 fireDirection) {
        if (clipCount > 0) {
            // We don't want to lose a bullet if we're shooting ourselves into the ground.
            bool useBullet = true;
            RaycastHit hit;
            //if (Physics.Raycast(player.position, fireDirection, out hit, 2)) {
            //    if (hit.collider.tag == "Ground")
            //        useBullet = false;
            //}
            //if (!(groundCheck.isGrounded && Mathf.Abs(playerRigidbody.velocity.y) < 0.01f)) {
            if (useBullet) { 
                clipCount--;
                if (UIStyle == GunUIStyles.IMAGEFILL) clipUI.fillAmount = (float)clipCount / clipSize;
                else if (UIStyle == GunUIStyles.BULLETS) RemoveOneBullet();
            }
            // Start an air reload if possible
            if (clipCount == 0 && airReloader != null) {
                Debug.Log("Air reload");
                airReloader.StartReload(GunManagement.instance.currentGun); // Could replace this with Pistol's gunType
            }
            return true;
        } else return false;
    }

    /// <summary>
    /// Resets the clip size.
    /// Right now this is handled on grounding, checked in this script, and called as needed from the appropriate gun.
    /// </summary>
    public void Reload() {
        clipCount = clipSize;
        if (UIStyle == GunUIStyles.IMAGEFILL) clipUI.fillAmount = 1;
        reloadPulseUI.Play();
        if (UIStyle == GunUIStyles.BULLETS) ReloadBulletUI();
    }

    public void SetClipSize(int size) {
        clipSize = size;
        Reload();
        if (UIStyle == GunUIStyles.BULLETS) ResetBulletUI();
    }

    public int GetClipCount() {
        return clipCount;
    }

    // ========== UI manip section =============
    // If the bullets aren't spawned in yet, spawn em in, then turn them all on.
    private void ResetBulletUI() {
        // Check if we got a higher clip or just spawned for the first time
        if (bullets.Count != clipSize) {
            // Destroy everything in the list
            foreach (var bullet in bullets)
                Destroy(bullet);
            // Then reset
            bullets = new List<GameObject>();
            // Then add and setup up to clipsize
            int columnNo = 0;
            int rowNo = 0;
            for (int i = 0; i < clipSize; i++) {
                GameObject bullet = Instantiate(bulletUIPrefab, BulletUIParent);
                // Figure out if the height would cross top, then if so add a column
                if ((rowNo + 1) * bulletUISeparation > BulletUITop.localPosition.y) {
                    rowNo = 0;
                    columnNo++;
                }
                bullet.GetComponent<RectTransform>().localPosition = new Vector3(columnNo * bulletUIHorSeparation, rowNo * bulletUISeparation, 0);
                //Debug.Log("Setting UI");
                bullets.Add(bullet);
                rowNo++;
            }
        }
    }

    // Real dumb name, but this just happens when you fire, it removes a bullet from clip UI
    private void RemoveOneBullet() {
        bullets[clipCount].SetActive(false);
    }

    private void ReloadBulletUI() {
        foreach (var bullet in bullets)
            bullet.SetActive(true);
    }
}
