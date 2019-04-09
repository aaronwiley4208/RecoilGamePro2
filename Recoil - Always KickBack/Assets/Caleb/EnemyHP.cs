using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour {

    public int maxHP = 5;
    public int currentHP = 5;

    private float canNextBeHurt = 0;
    public float invulnPeriod = 0;

	// Use this for initialization
	void Start () {
        currentHP = maxHP;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damage(int dam)
    {
        if (Time.time >= canNextBeHurt)
        {
            canNextBeHurt = Time.time + invulnPeriod;
            currentHP = currentHP - dam;
            //damage effects
            if (currentHP <= 0)
            {
                Kill();
            }
        }
    }

    public void Kill()
    {
        //any kill effects
        Destroy(this.gameObject);
    }
}
