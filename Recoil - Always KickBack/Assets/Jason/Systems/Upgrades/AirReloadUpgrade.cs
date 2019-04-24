using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirReloadUpgrade : MonoBehaviour, IUpgrade {
    [SerializeField]
    private int reloadAmount;
    [SerializeField]
    private Pistol gun;
    [SerializeField]
    [Tooltip("The UI Button that gets spawned to the list for this upgrade")]
    private GameObject uiPrefab;

    public void Equip() {
        gun.UpgradeAirReloads(reloadAmount);
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public GameObject GetUIPrefab() {
        return uiPrefab;
    }

    public float GetValue() {
        return reloadAmount;
    }

    public void SetGun(Pistol pistol) {
        gun = pistol;
    }

    public void Unequip() {
        gun.UpgradeAirReloads(-reloadAmount);
    }
}
