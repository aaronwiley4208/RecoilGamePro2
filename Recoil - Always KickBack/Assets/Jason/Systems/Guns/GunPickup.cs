﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour {

    public GunManagement.Guns gunType;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            print("Activating " + gunType);
            GunManagement.instance.ActivateGun(gunType);
            Destroy(gameObject);
        }
    }
}
