using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    GameObject obj;
    public ObjectPooler ObjectPooler;
    public float timer;
    float seconds = 0;

    // Use this for initialization
    void Start () {
        obj = this.gameObject;
        timer = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > timer + 1)
        {
            timer = Time.time;
            seconds++;           
        }

        if (seconds == 5)
        {
            ObjectPooler.Instance.ReturnObjectToPool(obj);
            seconds = 0;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "enemy") {            
            ObjectPooler.Instance.ReturnObjectToPool(obj);
            seconds = 0;
        }   
    }
}
