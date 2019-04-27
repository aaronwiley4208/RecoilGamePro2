using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bayonette : MonoBehaviour {

    [SerializeField]
    private float damage;
    [SerializeField]
    private float forceMultiplier;

    private Rigidbody playerRB;

	// Use this for initialization
	void Start () {
        playerRB = GunManagement.instance.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        Debug.Log("HiT SOMETHING");
        if (collision.collider.tag == "Enemy") {
            Debug.Log("Hit enemy");
            EnemyHP enemyHP = collision.collider.GetComponent<EnemyHP>();
            if (enemyHP) {
                // TODO: enemyHP.Damage((int)damage);
            }
            Rigidbody enemyRB = collision.collider.GetComponent<Rigidbody>();
            if (enemyRB) {
                enemyRB.AddForce(playerRB.velocity * playerRB.mass * forceMultiplier, ForceMode.Impulse);
            }
        }
    }
}
