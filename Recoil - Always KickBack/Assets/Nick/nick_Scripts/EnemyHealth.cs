using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int health = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
        {
            gameObject.SetActive(false);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider);
        if (col.collider.tag == "Player Bullet")
        {
            health -= 1;
            Debug.Log("Hit");
        }       
        
    }

}
