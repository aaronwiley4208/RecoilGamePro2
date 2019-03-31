using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newShoot : MonoBehaviour
{
    public Transform player;
    public float range = 50.0f;
    public float bulletImpulse = 10.0f;
    public GameObject cubePrefab;
    public ObjectPooler ObjectPooler;
    public int size;
    private bool onRange = false;

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
            GameObject temp = ObjectPooler.Instance.GetObjectFromPool();
            temp.SetActive(true);
            temp.transform.position = (transform.position);
            temp.transform.rotation = transform.rotation;
            temp.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            temp.transform.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * bulletImpulse, ForceMode.Impulse);  
        }
    }

    void Update()
    {
        onRange = Vector3.Distance(transform.position, player.position) < range;
    }    
}