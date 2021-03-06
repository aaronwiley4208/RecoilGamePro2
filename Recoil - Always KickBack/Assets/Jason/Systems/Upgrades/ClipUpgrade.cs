﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipUpgrade : MonoBehaviour, IUpgrade {
    [SerializeField]
    [Range(0, .2f)]
    private float clipPercentage;
    [SerializeField]
    private Pistol gun;
    [SerializeField]
    [Tooltip("The UI Button this should spawn when you get the upgrade")]
    private GameObject uiPrefab;

    public void Equip() {
        gun.UpgradeClip(clipPercentage);
    }

    public GameObject GetGameObject() {
        return gameObject;
    }

    public GameObject GetUIPrefab() {
        return uiPrefab;
    }

    public float GetValue() {
        return clipPercentage;
    }

    public void SetGun(Pistol pistol) {
        gun = pistol;
    }

    public void Unequip() {
        gun.UpgradeClip(-clipPercentage);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
