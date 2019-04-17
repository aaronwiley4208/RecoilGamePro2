using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUpgrade : MonoBehaviour, IUpgrade {
    [SerializeField]
    private float forceAmount;
    [SerializeField]
    private Pistol gun;
    [SerializeField]
    [Tooltip("The UI button this should use for its upgrade")]
    private GameObject uiPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Equip() {
        gun.UpgradeForce(forceAmount);
    }

    public void Unequip() {
        gun.UpgradeForce(-forceAmount);
    }

    public void SetGun(Pistol pistol) {
        gun = pistol;
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public GameObject GetUIPrefab() {
        return uiPrefab;
    }

    public float GetValue() {
        return forceAmount;
    }
}
