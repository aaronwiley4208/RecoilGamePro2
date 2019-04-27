using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayonetteScaleUpgrade : MonoBehaviour, IUpgrade {
    [SerializeField]
    [Range(0, .2f)]
    private float scaleAmount;
    [SerializeField]
    private Pistol gun;
    [SerializeField]
    [Tooltip("The UI button this should use for its upgrade")]
    private GameObject uiPrefab;

    public void Equip() {
        gun.UpgradeBayonetteScale(scaleAmount);
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public GameObject GetUIPrefab() {
        return uiPrefab;
    }

    public float GetValue() {
        return scaleAmount;
    }

    public void SetGun(Pistol pistol) {
        gun = pistol;
    }

    public void Unequip() {
        gun.UpgradeBayonetteScale(-scaleAmount);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
