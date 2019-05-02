using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the user to summon a bunch of upgrades
/// </summary>
public class UpgradeCheats : MonoBehaviour {

    [SerializeField]
    private List<GameObject> upgradeTypes;
    [SerializeField]
    private Transform playerObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)) {
            // Find a random upgrade
            GameObject upgrade = upgradeTypes[Mathf.RoundToInt(Random.Range(0, upgradeTypes.Count))];
            // Find the spot to put it in
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = playerObj.transform.position.z - Camera.main.transform.position.z;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

            Instantiate(upgrade, mousePos, Quaternion.identity);
        }
	}
}
