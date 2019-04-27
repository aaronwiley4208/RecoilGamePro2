using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour {

    public int maxHP = 5;
    public int currentHP = 5;

    private float canNextBeHurt = 0;
    public float invulnPeriod = 0;

    public bool isArmored;

    Material original;
    public Material hurtColor;

    // Use this for initialization
    void Start () {
        currentHP = maxHP;
        original = this.gameObject.GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Damage(int dam, bool piercing)  //type denotes standard/piercing, at 0 and 1 respectively
    {
        if (Time.time >= canNextBeHurt)
        {
            this.gameObject.GetComponent<Renderer>().material = original;
            if (isArmored)
            {
                if (piercing)
                {
                    canNextBeHurt = Time.time + invulnPeriod;
                    currentHP = currentHP - dam;
                    if (currentHP <= 0)
                    {
                        Kill();
                    }
                    else
                    {
                        this.gameObject.GetComponent<Renderer>().material = hurtColor;
                        //put here to prevent damage/kill overlaps
                    }
                }
                else
                {
                    //do plink noise
                }
            }
            else
            {
                canNextBeHurt = Time.time + invulnPeriod;
                currentHP = currentHP - dam;
                if (currentHP <= 0)
                {
                    Kill();
                }
                else
                {
                    this.gameObject.GetComponent<Renderer>().material = hurtColor;
                    //put here to prevent damage/kill overlaps
                }
            }
        }
    }

    public void Kill()
    {
        //any kill effects
        Destroy(this.gameObject);
    }
}
