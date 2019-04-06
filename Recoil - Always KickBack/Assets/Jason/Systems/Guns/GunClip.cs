using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunClip : MonoBehaviour {

    [Header("Clip Info")]
    [SerializeField]
    [Tooltip("The max clip size for this gun")]
    private int clipSize;
    [SerializeField]
    private int clipCount;

    [Header("UI")]
    [SerializeField]
    [Tooltip("The UI image must have a fill amount.")]
    private Image clipUI;
    [SerializeField]
    Animation reloadPulseUI;

	// Use this for initialization
	void Start () {
        Reload();
	}
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// Checks against the current clip count, tells the caller
    /// if this gun can fire.
    /// </summary>
    /// <returns>Whether or not the gun can fire.</returns>
    public bool Fire() {
        if (clipCount > 0) {
            clipCount--;
            clipUI.fillAmount = (float)clipCount / clipSize;
            return true;
        } else return false;
    }

    /// <summary>
    /// Resets the clip size.
    /// Right now this is handled on grounding, checked in this script, and called as needed from the appropriate gun.
    /// </summary>
    public void Reload() {
        clipCount = clipSize;
        clipUI.fillAmount = 1;
        reloadPulseUI.Play();
    }

    public void SetClipSize(int size) {
        clipSize = size;
        Reload();
    }

    public int GetClipCount() {
        return clipCount;
    }
}
