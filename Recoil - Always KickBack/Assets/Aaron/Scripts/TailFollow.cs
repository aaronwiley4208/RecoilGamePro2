using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailFollow : MonoBehaviour {

    public GameObject head;
    public float followDis;

    float timer = 0;
    Vector3 followPos;

	void Start () {
		
	}
	
	
	void Update () {
        if (timer >= 0.5f) {
            if (followDis < (head.transform.position - transform.position).magnitude) {
                followPos = head.transform.position;
            }            
            timer = 0;
        }
        if (0.5f < timer) {
            timer += Time.deltaTime;
        }
        transform.position = Vector3.MoveTowards(transform.position, followPos, 2 * Time.deltaTime);

	}


}
