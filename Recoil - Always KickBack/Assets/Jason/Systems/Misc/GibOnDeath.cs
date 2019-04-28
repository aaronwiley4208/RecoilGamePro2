using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds a function that will shoot a bunch of gibs in a random explosion on death
/// </summary>
public class GibOnDeath : MonoBehaviour {

    [SerializeField]
    private GameObject gib;
    [SerializeField]
    private int numGibs = 10;
    [SerializeField]
    private float startingRadius = 0.5f;
    [SerializeField]
    private float explosionForce = 10;
    [SerializeField]
    private float upwardsForce = 40;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) Gib();
	}

    public void Gib() {
        // Spawn gibs
        for (int i = 0; i < numGibs; i++) {
            Vector3 startPos = (Random.onUnitSphere * startingRadius) + transform.position;
            GameObject g = Instantiate(gib, startPos, Random.rotation, transform);
            g.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, 2, upwardsForce, ForceMode.Impulse);
        } 
    }

    // Gib, but one frame at a time
    public void DelayGib() {
        StartCoroutine(GibOverFrames());
    }

    IEnumerator GibOverFrames() {
        for (int i = 0; i < numGibs; i++) {
            Vector3 startPos = (Random.onUnitSphere * startingRadius) + transform.position;
            GameObject g = Instantiate(gib, startPos, Random.rotation, transform);
            g.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, 2, upwardsForce, ForceMode.Impulse);
            yield return null;
        }
    }
}
