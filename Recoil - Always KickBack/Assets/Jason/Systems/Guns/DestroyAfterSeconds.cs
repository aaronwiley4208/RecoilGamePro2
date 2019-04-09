using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    [SerializeField]
    private float timeTilDeath = 5;

	// Use this for initialization
	void Start () {
        StartCoroutine("DestroyAfterTime");
	}
	
    IEnumerator DestroyAfterTime() {
        yield return new WaitForSeconds(timeTilDeath);
        Destroy(gameObject);
    }
}
