using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRight : MonoBehaviour {

    public float windSpeed = 10f;
    public ParticleSystem wind;


    ParticleSystem.MainModule windMain;

    void Start() {
        windMain = wind.main;
        windMain.startSpeed = windSpeed / 50;
        windMain.startLifetime = transform.parent.localScale.x *  1350 / windSpeed;
        var windEmit = wind.emission;
        windEmit.rateOverTime = windSpeed / 500; 
        
    }

    void OnTriggerStay(Collider col){
		if(col.tag == "Player"){

            col.GetComponent<Rigidbody>().AddForce(transform.right * windSpeed, ForceMode.Force);
            
		}
	}
}
