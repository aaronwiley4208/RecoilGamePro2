using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public float HP = 3;
	public float barrierStr = 3;
    public ParticleSystem deathExpl;
    public ParticleSystem barrierEffect;
	ParticleSystem.MainModule barrierMain;
	public GameObject heart1;
	public GameObject heart2;
	public GameObject heart3;

    public float rechargeTimer = 3;
    public float barrierPsec = 0.1f;

    bool barrierActive = false;
    bool gotHit = false;

    // Use this for initialization
    void Start () {
		barrierMain = barrierEffect.main;
		barrierMain.startColor = Color.green;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0) {
            StartCoroutine(Die());
        }	    
		if (barrierStr > 0) {
            barrierEffect.Play();
            barrierMain.startColor = Color.Lerp(Color.red, Color.green, barrierStr/3);
		} else {
			barrierEffect.Stop();
		}

        
	}
    //doesnt reset if hit during cooldown, not sure how to fix yet
    IEnumerator BarrierRegen() {        
        while(barrierActive) {
            if (gotHit){
                gotHit = false;
                yield return new WaitForSeconds(rechargeTimer);                
            }
            if (barrierStr < 3){
                barrierStr += barrierPsec;
                yield return new WaitForSeconds(1);
            }
            else {
                yield return null;
            }
        }
    }

    IEnumerator Die() {
        print("D E A D T");
        deathExpl.Play();
        StopCoroutine(BarrierRegen());
        GetComponent<GunHolder>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        yield return new WaitForSeconds(2);
        GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().detectCollisions = false;
        deathExpl.Stop();

    }

    private void OnCollisionEnter(Collision col)
    {
		if (col.gameObject.layer == 11) {
			if (barrierStr > 0) {
				barrierStr = barrierStr - 1;
			} else {
				HP = HP - 1;
				if (heart1.activeSelf == true) {
					heart1.SetActive (false);
				} else if (heart2.activeSelf == true) {
					heart2.SetActive (false);
				} else {
					heart3.SetActive (false);
				}
			}            
            print(HP);
		}if(col.gameObject.name == "BarrierPickUp"){
			Destroy(col.gameObject);
            barrierActive = true;
            StartCoroutine(BarrierRegen());
            if (barrierStr < 3){
				barrierStr = 3;
				barrierEffect.Play ();
			}
		}
        
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.layer == 10){
			if (barrierStr > 0) {
				barrierStr = barrierStr - 1;
                gotHit = true;
                if (barrierStr <= 0) {
                    barrierStr = 0;
                }
			} else {
				HP = HP - 1;
				if (heart1.activeSelf == true) {
					heart1.SetActive (false);
				} else if (heart2.activeSelf == true) {
					heart2.SetActive (false);
				} else {
					heart3.SetActive (false);
				}
			}
        }
    }
}
