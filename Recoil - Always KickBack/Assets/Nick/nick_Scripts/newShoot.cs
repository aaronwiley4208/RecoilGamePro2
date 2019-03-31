using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newShoot : MonoBehaviour
{

    public Transform player;
    public float range = 50.0f;
    public float bulletImpulse = 20.0f;

    //testing object pooler
    public GameObject cubePrefab;
    public ObjectPooler ObjectPooler;
    public int size;

    private bool onRange = false;
    public Rigidbody projectile;

    void Start()
    {
        float rand = Random.Range(1.0f, 2.0f);

        //testing object pooler
        InvokeRepeating("Shoot", 2, rand);
        ObjectPooler = new ObjectPooler(cubePrefab, size);
    }

    void Shoot()
    {

        if (onRange)
        {
            //object pooler
            /*GameObject temp = ObjectPooler.Instance.GetObjectFromPool();
            temp.SetActive(true);
            temp.transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
            */

            Rigidbody bullet = (Rigidbody)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
            bullet.AddForce(transform.forward * bulletImpulse, ForceMode.Impulse);

        }
    }

    void Update()
    {
        onRange = Vector3.Distance(transform.position, player.position) < range;

        if (onRange)
        {
            transform.LookAt(player);
        }
    }    
}