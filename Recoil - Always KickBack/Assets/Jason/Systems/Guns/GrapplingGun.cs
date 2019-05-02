using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The gun part of the grappling system. Shoots a hook.
/// </summary>
public class GrapplingGun : MonoBehaviour {

    [SerializeField]
    private GameObject hookPrefab;
    [SerializeField]
    private Color[] hookColors;
    [SerializeField]
    private float shotForce;
    [SerializeField]
    private Transform player;
    [SerializeField]
    private bool isGrappling;
    [SerializeField]
    private int numHooks;
    [SerializeField]
    private GunClip hookClip;
    [SerializeField]
    private GameObject grapplingUI;

    private GameObject[] hooks;
    private SpringJoint[] springs;
    private LineRenderer[] ropes;

    public static GrapplingGun instance;

    private void Awake() {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    // Use this for initialization
    void Start () {
        ropes = new LineRenderer[2];
        hooks = new GameObject[2];
        springs = new SpringJoint[2];
        hookClip.SetClipSize(numHooks);
	}
	
	// Update is called once per frame
	void Update () {
        // Get direction
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = player.position.z - Camera.main.transform.position.z;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        Vector3 playerToMouse = (mousePos - player.position).normalized;

        if (Input.GetKeyDown(KeyCode.E)) {
            if (hookClip.Fire(-playerToMouse))
                Grapple(playerToMouse, 0);
        }
        if (Input.GetKeyUp(KeyCode.E)) {
                DetachGrapple(0);
        }

        // Check For second grapple
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (hookClip.Fire(-playerToMouse))
                Grapple(playerToMouse, 1);
        }
        if (Input.GetKeyUp(KeyCode.Q)) {
                DetachGrapple(1);
        }

        // Update Rope
        for (int i = 0; i < ropes.Length; i++) {
            if (ropes[i] != null && ropes[i].enabled) {
                ropes[i].SetPosition(0, player.position);
                ropes[i].SetPosition(1, hooks[i].transform.position);
            }

        }

        // Cheats yo
        if (Input.GetKeyDown(KeyCode.G))
            AddHook();
	}

    private void Grapple(Vector3 direction, int index) {
        isGrappling = true;
        // spawn a hook
        hooks[index] = Instantiate(hookPrefab, player.position + direction * 2, Quaternion.identity);
        hooks[index].GetComponent<Renderer>().material.color = hookColors[index];
        Rigidbody hookRB = hooks[index].GetComponent<Rigidbody>();
        // Put a joint on the player
        springs[index] = player.gameObject.AddComponent<SpringJoint>();
        springs[index].spring = 50;
        springs[index].autoConfigureConnectedAnchor = false;
        springs[index].connectedBody = hookRB;
        springs[index].connectedAnchor = Vector3.zero;
        //springs[index].breakForce = 4002; TODO: Investigate Break force
        // Fire the hook
        hookRB.AddForce(direction * shotForce, ForceMode.Impulse);
        // Set the rope
        ropes[index] = hooks[index].GetComponent<LineRenderer>();
    }

    private void DetachGrapple(int index) {
        isGrappling = false;
        if (ropes[index] != null)
            ropes[index].enabled = false;
        //ropes.enabled = false;
        if (springs[index] != null)
            Destroy(springs[index]);
    }

    /// <summary>
    /// Add a hook to your inventory, only can add one at a time.
    /// </summary>
    public void AddHook() {
        grapplingUI.SetActive(true); // This is bad way to do this, should check for num hooks on start.
        numHooks++;
        hookClip.SetClipSize(numHooks);
    }

    // Remove all grapples
    public void DeGrapple() {
        DetachGrapple(0); DetachGrapple(1);
    }
}
