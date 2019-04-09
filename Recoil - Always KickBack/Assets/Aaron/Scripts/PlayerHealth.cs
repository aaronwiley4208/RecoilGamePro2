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

    // Use this for initialization
    void Start () {
		barrierMain = barrierEffect.main;
		barrierMain.startColor = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
        if (HP <= 0) {
            print("D E A D T");
			deathExpl.Play();
        }
	
		if (barrierStr >= 3) {
			barrierMain.startColor = Color.green;
		} else if (barrierStr == 2) {
			barrierMain.startColor = Color.yellow;
		} else if (barrierStr == 1) {
			barrierMain.startColor = Color.red;
		} else {
			barrierEffect.Stop();
		}

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
			if(barrierStr < 3){
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
        }
    }
}
