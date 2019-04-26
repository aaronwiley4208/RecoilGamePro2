using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayonetteUpgrade : MonoBehaviour, IUpgrade {
    [SerializeField]
    [Tooltip("How many bayonettes to add")]
    private int bayonetteCount;
    [SerializeField]
    private Pistol gun;
    [SerializeField]
    [Tooltip("The UI button this should use for its upgrade")]
    private GameObject uiPrefab;

    public void Equip() {
        gun.UpgradeBayonette(bayonetteCount);
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public GameObject GetUIPrefab() {
        return uiPrefab;
    }

    public float GetValue() {
        return (float)bayonetteCount;
    }

    public void SetGun(Pistol pistol) {
        gun = pistol;
    }

    public void Unequip() {
        gun.UpgradeBayonette(-bayonetteCount);
    }
}
