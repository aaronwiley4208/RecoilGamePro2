﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRight : MonoBehaviour {

    public float windSpeed = 10f;

	void OnTriggerStay(Collider col){
		if(col.tag == "Player"){
            col.GetComponent<Rigidbody>().AddForce(Vector3.right * windSpeed, ForceMode.Force);
            //print("We in");
		}
	}
}
