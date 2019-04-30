using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour {

    public GameObject winUI;


    void Start() {
        winUI.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Player") {
            winUI.SetActive(true);
            collision.transform.GetComponent<PlayerGroundMovement>().enabled = false;
            collision.transform.GetComponent<GunHolder>().enabled = false;
            
        }
    }


}
