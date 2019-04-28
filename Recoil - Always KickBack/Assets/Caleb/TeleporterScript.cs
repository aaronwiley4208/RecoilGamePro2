using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if (true)  //player check here
        {
            //col.gameObject.transform.position = col.getComponent<PlayerScript>().lastGroundedPosition;
        } else if (true)  //enemy check here
        {
            col.GetComponent<EnemyHP>().Kill();  //enemies explode in teleporters
        }
    }

    private void OnTriggerStay(Collider col)  //just in case
    {
        if (true)  //player check here
        {
            //col.gameObject.transform.position = col.getComponent<PlayerScript>().lastGroundedPosition;
        }
        else if (true)  //enemy check here
        {
            col.GetComponent<EnemyHP>().Kill();  //enemies explode in teleporters
        }
    }

    private void OnTriggerExit(Collider col)  //really just in case
    {
        if (true)  //player check here
        {
            //col.gameObject.transform.position = col.getComponent<PlayerScript>().lastGroundedPosition;
        }
        else if (true)  //enemy check here
        {
            col.GetComponent<EnemyHP>().Kill();  //enemies explode in teleporters
        }
    }
}
