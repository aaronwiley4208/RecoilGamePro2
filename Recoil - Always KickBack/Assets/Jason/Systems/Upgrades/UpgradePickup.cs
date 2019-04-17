using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines upgrade behavior when touched. Just adds it to the player's upgrade inventory.
/// </summary>
public class UpgradePickup : MonoBehaviour {

    public IUpgrade upgrade;

    private void Start() {
        upgrade = GetComponentInChildren<IUpgrade>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Adding: " + upgrade.GetGameObject().name);
            UpgradeToolbelt.instance.AddUpgrade(upgrade);
            Destroy(gameObject);
        }
    }
}
