using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages switching guns and updating necessary scripts when doing so.
/// </summary>
public class GunManagement : MonoBehaviour {
    public enum Guns { PISTOL, SHOTGUN, MACHINEGUN, SNIPER }

    [SerializeField]
    public Guns currentGun;

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
    [SerializeField]
    private GameObject sniper;

    [Header("UI")]
    [SerializeField]
    private float selectedOutlineWidth;
    [SerializeField]
    private Outline pistolSelection;
    [SerializeField]
    private Outline shotgunSelection;
    [SerializeField]
    private Outline machinegunSelection;
    [SerializeField]
    private Outline sniperSelection;

    public Dictionary<Guns, GameObject> guns;
    private Dictionary<Guns, Outline> outlines;

    public static GunManagement instance;

    private void Awake() {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    // Use this for initialization
    void Start () {
        guns = new Dictionary<Guns, GameObject>();
        guns.Add(Guns.PISTOL, pistol);
        guns.Add(Guns.SHOTGUN, shotgun);
        guns.Add(Guns.MACHINEGUN, machinegun);
        guns.Add(Guns.SNIPER, sniper);
        outlines = new Dictionary<Guns, Outline>();
        outlines.Add(Guns.PISTOL, pistolSelection);
        outlines.Add(Guns.SHOTGUN, shotgunSelection);
        outlines.Add(Guns.MACHINEGUN, machinegunSelection);
        outlines.Add(Guns.SNIPER, sniperSelection);

        //ChangeGun(currentGun);
	}
	
	// Update is called once per frame
	void Update () {
        // Check for wheel
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
		if (wheelInput < 0) {
            //currentGun = (Guns)(((int)(currentGun + 1)) % System.Enum.GetValues(typeof(Guns)).Length);
            int nextGun = GetNextGun();
            if (nextGun >= 0) ChangeGun((Guns)nextGun);
        } else if (wheelInput > 0) {
            //currentGun = (Guns)(((int)(currentGun - 1)) % System.Enum.GetValues(typeof(Guns)).Length);
            int nextGun = GetPrevGun();
            if (nextGun >= 0) ChangeGun((Guns)nextGun);
        }
        // Check for Numpad inputs
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            if (guns[Guns.PISTOL].GetComponent<Pistol>().active) {
                currentGun = Guns.PISTOL; ChangeGun(Guns.PISTOL);
            }
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (guns[Guns.SHOTGUN].GetComponent<Pistol>().active) {
                currentGun = Guns.SHOTGUN; ChangeGun(Guns.SHOTGUN);
            }
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (guns[Guns.MACHINEGUN].GetComponent<Pistol>().active) {
                currentGun = Guns.MACHINEGUN; ChangeGun(Guns.MACHINEGUN);
            }
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            if (guns[Guns.SNIPER].GetComponent<Pistol>().active) {
                currentGun = Guns.SNIPER; ChangeGun(Guns.SNIPER);
            }
        }

        // Check for cheating getting guns
        if (Input.GetKeyDown(KeyCode.Keypad1)) {
            ActivateGun(Guns.PISTOL);
        } else if (Input.GetKeyDown(KeyCode.Keypad2)) {
            ActivateGun(Guns.SHOTGUN);
        } else if (Input.GetKeyDown(KeyCode.Keypad3)) {
            ActivateGun(Guns.MACHINEGUN);
        } else if (Input.GetKeyDown(KeyCode.Keypad4)) {
            ActivateGun(Guns.SNIPER);
        }
    }


    private void ChangeGun(Guns gun) {
        currentGun = gun;
        holder.gunObj = guns[gun].transform;
        // Go through the guns and turn off the ones we're not using.
        foreach (Guns g in System.Enum.GetValues(typeof(Guns))) {
            if (g == gun) { guns[g].SetActive(true); outlines[g].effectDistance = new Vector2(selectedOutlineWidth, 1); } 
            else { guns[g].SetActive(false); outlines[g].effectDistance = new Vector2(1, 1); }
        }
    }

    /// <summary>
    /// Get the Gameobject for the current gun
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentGun() {
        return guns[currentGun];
    }

    /// <summary>
    /// Activates the chosen gun
    /// </summary>
    public void ActivateGun(Guns gun) {
        guns[gun].GetComponent<Pistol>().Activate();
        ChangeGun(gun);
    }

    // Get the next active gun. If no guns active, return an impossible value
    private int GetNextGun() {
        // Cycle once for each type of gun, checking for active status
        Guns nextGun = currentGun;
        for (int i = 0; i < System.Enum.GetValues(typeof(Guns)).Length; i++) {
            nextGun = (Guns)Mathf.Repeat((float)nextGun + 1, System.Enum.GetValues(typeof(Guns)).Length);
            if (guns[nextGun].GetComponent<Pistol>().active) return (int)nextGun;
        }
        return -1;
    }

    private int GetPrevGun() {
        // Cycle once for each type of gun, checking for active status
        Guns nextGun = currentGun;
        for (int i = 0; i < System.Enum.GetValues(typeof(Guns)).Length; i++) {
            nextGun = (Guns)Mathf.Repeat((float)nextGun + 1, System.Enum.GetValues(typeof(Guns)).Length);
            if (guns[nextGun].GetComponent<Pistol>().active) return (int)nextGun;
        }
        return -1;
    }
}
