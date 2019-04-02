using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float HP = 3;
    public ParticleSystem deathExpl;
    public ParticleSystem barrierEffect;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0) {
            print("D E A D T");
            deathExpl.Emit(1);
        }
	}

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "enemy") {
            HP = HP - 1;
            print(HP);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Bullet")){
            HP = HP - 1;
            print(HP);
        }
    }
}
