using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {

    public int damage = 5;
    public bool piercing = false;

    bool isDead = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision col)
    {
        if (!isDead)
        {
            isDead = true;  //bullet stops being deadly on collision; becomes dead bullet
            if (true)  //enemy tag check goes here
            {
                col.gameObject.GetComponent<EnemyHP>().Damage(damage, piercing);
                if (piercing)
                {
                    isDead = false;  //bullet pierces enemies, does not actually die
                }
            }
        }
    }
}
