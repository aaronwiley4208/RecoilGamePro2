using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages switching guns and updating necessary scripts when doing so.
/// </summary>
public class GunManagement : MonoBehaviour {
    public enum Guns { PISTOL, SHOTGUN, MACHINEGUN }

    [SerializeField]
    private Guns currentGun;

    [Header("Player Attributes")]
    [SerializeField]
    private GunHolder holder;
    [SerializeField]
    private ReloadOnGround reload;

    [Header("Guns")]
    [SerializeField]
    private GameObject pistol;
    [SerializeField]
    private GameObject shotgun;
    [SerializeField]
    private GameObject machinegun;

    [Header("UI")]
    [SerializeField]
    private float selectedOutlineWidth;
    [SerializeField]
    private Outline pistolSelection;
    [SerializeField]
    private Outline shotgunSelection;
    [SerializeField]
    private Outline machinegunSelection;

    private Dictionary<Guns, GameObject> guns;
    private Dictionary<Guns, Outline> outlines;

	// Use this for initialization
	void Start () {
        guns = new Dictionary<Guns, GameObject>();
        guns.Add(Guns.PISTOL, pistol);
        guns.Add(Guns.SHOTGUN, shotgun);
        guns.Add(Guns.MACHINEGUN, machinegun);
        outlines = new Dictionary<Guns, Outline>();
        outlines.Add(Guns.PISTOL, pistolSelection);
        outlines.Add(Guns.SHOTGUN, shotgunSelection);
        outlines.Add(Guns.MACHINEGUN, machinegunSelection);

        ChangeGun(currentGun);
	}
	
	// Update is called once per frame
	void Update () {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (wheelInput > 0) {
            //currentGun = (Guns)(((int)(currentGun + 1)) % System.Enum.GetValues(typeof(Guns)).Length);
            currentGun = (Guns)Mathf.Repeat((float)currentGun + 1, System.Enum.GetValues(typeof(Guns)).Length);
            ChangeGun(currentGun);
        } else if (wheelInput < 0) {
            //currentGun = (Guns)(((int)(currentGun - 1)) % System.Enum.GetValues(typeof(Guns)).Length);
            currentGun = (Guns)Mathf.Repeat((float)currentGun - 1, System.Enum.GetValues(typeof(Guns)).Length);
            ChangeGun(currentGun);
        }
    }


    private void ChangeGun(Guns gun) {
        holder.gunObj = guns[gun].transform;
        // Go through the guns and turn off the ones we're not using.
        foreach (Guns g in System.Enum.GetValues(typeof(Guns))) {
            if (g == gun) { guns[g].SetActive(true); outlines[g].effectDistance = new Vector2(selectedOutlineWidth, 1); } 
            else { guns[g].SetActive(false); outlines[g].effectDistance = new Vector2(1, 1); }
        }
    }
}
