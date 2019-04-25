using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An all-in-one for upgrading the grappler, since its only upgrade is a clip upgrade.
/// </summary>
public class UpgradeGrapple : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GrapplingGun.instance.AddHook();
            Destroy(gameObject);
        }
    }
}
