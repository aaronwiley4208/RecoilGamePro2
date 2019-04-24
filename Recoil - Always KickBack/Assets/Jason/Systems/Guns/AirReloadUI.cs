using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the UI for Air reloads on each gun.
/// This goes on the gun itself instead of the player with the ReloadInAir script.
/// </summary>
public class AirReloadUI : MonoBehaviour {
    [SerializeField]
    private GameObject reloadIconPrefab;
    [SerializeField]
    private float verticalSeparation;
    [SerializeField]
    [Tooltip("The reload icons will be parented to this transform")]
    private RectTransform uiParent;

    // A collection of all the reload icons
    public List<GameObject> reloads = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Sets the given number of reload icons next to the gun's UI.
    /// </summary>
    /// <param name="numReloads">The number of reloaads this gun now has.</param>
    public void SetReloads(int numReloads) {
        // First destroy all existing icons
        foreach (var reload in reloads)
            Destroy(reload);
        // Then reset the list
        reloads = new List<GameObject>();
        // Then add and place icons
        for (int i = 0; i < numReloads; i++) {
            GameObject reload = Instantiate(reloadIconPrefab, uiParent); // Spawn in
            reload.GetComponent<RectTransform>().localPosition = new Vector3(0, i * verticalSeparation, 0);  // Move up
            reloads.Add(reload);
        }
    }

    /// <summary>
    /// Deactivates the Reload icon of the given index. Should always be the reloadCount.
    /// </summary>
    /// <param name="index"></param>
    public void UseOneReload(int index) {
        reloads[index].SetActive(false);
    }

    /// <summary>
    /// Make all images reload icons active again
    /// </summary>
    public void ResetUI() {
        foreach (var reload in reloads)
            reload.SetActive(true);
    }
}
