using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the players upgrade items until the player decides to put them on a gun.
/// Also controls a few aspects of the UI.
/// </summary>
public class UpgradeToolbelt : MonoBehaviour {

    [SerializeField]
    [Tooltip("Reference to the belt object, so we can parent upgrades to it.")]
    private Transform beltObject;
    [SerializeField]
    private List<IUpgrade> upgrades;
    [SerializeField]
    private List<GameObject> upgradeUIs;

    [Header("UI Fields")]
    [SerializeField]
    private GameObject toolbeltUI;
    [SerializeField]
    private RectTransform upgradeList;
    [Tooltip("The positions of the Upgrade's UIs will be based on these two rects.")]
    [SerializeField]
    private RectTransform firstPos;
    [SerializeField]
    private RectTransform secondPos;

    [Header("Menu Selection Fields")]
    [SerializeField]
    private int currentUpgrade;

    public static UpgradeToolbelt instance;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        upgrades = new List<IUpgrade>();
        upgradeUIs = new List<GameObject>();
	}

    // Update is called once per frame
    void Update() {
        // Open up belt UI
        if (Input.GetKeyDown(KeyCode.Tab)) {
            toolbeltUI.SetActive(true);
            Time.timeScale = 0;
        } else if (Input.GetKeyUp(KeyCode.Tab)) {
            toolbeltUI.SetActive(false);
            Time.timeScale = 1;
        }

	}

    public void AddUpgrade(IUpgrade upgrade) {
        // Transfer the thing physically so it doesn't get destroyed by the pickup going away.
        upgrade.GetGameObject().transform.parent = beltObject;
        upgrades.Add(upgrade);
        PopulateUI();
        // TEMP: Add the upgrade to the current gun
        //upgrade.GetGameObject().transform.parent = GetComponent<GunManagement>().GetCurrentGun().transform;
        //upgrade.SetGun(GetComponent<GunManagement>().GetCurrentGun().GetComponent<Pistol>());
        //upgrade.Equip();
    }

    // Runs through the list of available upgrades and puts their images on the upgrade panel.
    private void PopulateUI() {
        // First remove all the old upgrades
        foreach (GameObject upgrade in upgradeUIs) {
            Destroy(upgrade);
        }
        upgradeUIs = new List<GameObject>();

        float verticalDistance = Mathf.Abs(firstPos.position.y - secondPos.position.y);
        for (int i = 0; i < upgrades.Count; i++) {
            // Instantiate the upgrade
            GameObject upgrade = Instantiate(upgrades[i].GetUIPrefab(), upgradeList);
            upgrade.transform.position = firstPos.position - new Vector3(0, i * verticalDistance, 0);
            // Update the value text. Assume if it's less than 1 that it's a percentage
            if (upgrades[i].GetValue() < 1) // percentage
                upgrade.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "* " + upgrades[i].GetValue() * 100 + "%";
            else // some added value
                upgrade.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "+ " + upgrades[i].GetValue();

            upgradeUIs.Add(upgrade);
        }
    }

    /// <summary>
    /// Equip the 0th upgrade to the selected gun.
    /// </summary>
    /// <param name="gunType"></param>
    public void EquipTopUpgrade(int gunType) {
        IUpgrade upgrade = upgrades[0];
        // Get the gun object and parent the upgrade to it. This actually doesn't really serve a point... but it may later
        upgrade.GetGameObject().transform.parent = GunManagement.instance.guns[(GunManagement.Guns)gunType].transform;
        upgrade.SetGun(GunManagement.instance.guns[(GunManagement.Guns)gunType].GetComponent<Pistol>());
        upgrade.Equip();
        // Remove the upgrade from our list
        upgrades.RemoveAt(0);
        PopulateUI();
    }
}
