using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An abstract class that all other guns should inherit from. 
/// Defines the basic things that all guns should have. Other guns will make new fields.
/// This object will be put on the gun object itself.
/// THIS SCRIPT IS NOW BUNK
/// </summary>
public abstract class Gun : MonoBehaviour {

    // Link to the gun barrel, which actually fires the thing
    public GunBarrel gunBarrel;
    // Link to the recoil movement which does the player movement
    public PlayerRecoilMovement recoilMovement;

    [Header("Gun Properties")]
    [SerializeField]
    [Tooltip("How many shots the player can fire before reloading")]
    protected int clipSize;
    [SerializeField]
    [Tooltip("The backwards force the gun exerts on the player")]
    protected float recoilForce;

    [Header("Player Properties")]
    [SerializeField]
    [Tooltip("The player's object")]
    protected Transform playerObj;
    [SerializeField]
    [Tooltip("How far away the gun is held from the player")]
    protected float gunToPlayerDistance;

    // Since this is gonna be used a lot, just keep it global
    protected Vector3 playerToGun;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public virtual void Update () {
		// Update the gun's position
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = playerObj.position.z - Camera.main.transform.position.z;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector3 playerToMouse = playerObj.position - mousePos;
        transform.position = playerObj.position - (playerToMouse.normalized * gunToPlayerDistance);
        playerToGun = playerToMouse;
    }

    public void Fire() {
        
    }
}
