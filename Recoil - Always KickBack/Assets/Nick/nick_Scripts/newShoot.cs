using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newShoot : MonoBehaviour
{
    //if no object is set in editor load default projectile
    public GameObject projectile;
    public float range = 50.0f;
    public float bulletImpulse = 10.0f;   
    private bool onRange = false;
    private Transform player;
    private int layerMask;
    private RaycastHit hit;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        int enemyLayerIndex = LayerMask.NameToLayer("Enemies");
        int bulletLayerIndex = LayerMask.NameToLayer("Bullets");
        layerMask = (1 << enemyLayerIndex) | (1 << bulletLayerIndex);
        layerMask = ~layerMask;
    }
    void Start()
    {        
        float rand = Random.Range(1.5f, 2.0f);
        InvokeRepeating("Shoot", 2, rand);       
    }

    void Shoot()
    {
        if (onRange)
        {
            GameObject temp = ObjectPool.SharedInstance.GetPooledObject(projectile.tag);
            temp.transform.position = transform.position;
            temp.transform.rotation = transform.rotation;
            temp.SetActive(true);
            temp.transform.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * bulletImpulse, ForceMode.Impulse);
            
        }
    }

    void Update()
    {                       
        if (Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, out hit, range, layerMask))
        {
            if (hit.collider.tag == "Player")
            {
                onRange = true;
            }
            else
            {
                onRange = false;
            }
        }

        if(onRange == true)
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }
        else
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.red);
        }

    }
}