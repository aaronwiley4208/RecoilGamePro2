using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newShoot : MonoBehaviour
{
    //vars to shoot
    public Transform player;
    public float range = 50.0f;
    public float bulletImpulse = 10.0f;
    private bool onRange = false;

    //object pool
    public GameObject cubePrefab;
    public ObjectPooler ObjectPooler;
    public int size;
    
    void Start()
    {
        float rand = Random.Range(1.0f, 2.0f);
        InvokeRepeating("Shoot", 2, rand);
        ObjectPooler = new ObjectPooler(cubePrefab, size);
    }

    void Shoot()
    {
        if (onRange)
        {   
            //initialzing and resetting object from pool
            GameObject temp = ObjectPooler.Instance.GetObjectFromPool();
            temp.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;

            //set active and send towards player
            temp.SetActive(true);
            temp.transform.position = transform.position;
            temp.transform.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * bulletImpulse, ForceMode.Impulse);  
        }
    }

    void Update()
    {
        //boolean to check if player is within attack range
        onRange = Vector3.Distance(transform.position, player.position) < range;
    }    
}