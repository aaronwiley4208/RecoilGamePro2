using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunClip : MonoBehaviour {

    [SerializeField]
    private int clipSize;
    private int clipCount;

	// Use this for initialization
	void Start () {
        clipCount = clipSize;
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
            return true;
        } else return false;
    }

    /// <summary>
    /// Resets the clip size.
    /// </summary>
    public void Reload() {

    }

    public void SetClipSize(int size) {
        clipSize = size;
        clipCount = size;
    }
}
