using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newShoot : MonoBehaviour
{    
    public float range = 50.0f;
    public float bulletImpulse = 10.0f;
    private Transform player;
    private bool onRange = false;

    private void Awake()
    {
         player = GameObject.FindWithTag("Player").transform;
    }
    void Start()
    {
        float rand = Random.Range(1.0f, 2.0f);
        InvokeRepeating("Shoot", 2, rand);       
    }

    void Shoot()
    {
        if (onRange)
        {            
            GameObject temp = ObjectPool.SharedInstance.GetPooledObject();
            temp.transform.position = transform.position;
            temp.transform.rotation = transform.rotation;
            temp.SetActive(true);
            temp.transform.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * bulletImpulse, ForceMode.Impulse);
        }
    }

    void Update()
    {
        //boolean to check if player is within attack range
        onRange = Vector3.Distance(transform.position, player.position) < range;
    }
}