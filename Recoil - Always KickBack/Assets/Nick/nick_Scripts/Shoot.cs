using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public float range = 50.0f;
    public float bulletImpulse = 10.0f;
    private Transform player;
    private int layerMask;

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
        float rand = Random.Range(1.0f, 2.0f);
        StartCoroutine("Fire", rand);       
    }

    void Update()
    {
    }

    IEnumerator Fire(float rand)
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, (player.transform.position - transform.position).normalized, out hit, range, layerMask);

            if (hit.collider.tag == "Player")
            {
                GameObject temp = ObjectPool.SharedInstance.GetPooledObject(projectile.tag);
                temp.transform.position = transform.position;
                temp.transform.rotation = transform.rotation;
                temp.SetActive(true);
                temp.transform.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * bulletImpulse, ForceMode.Impulse);

            }
            yield return new WaitForSeconds(rand);
        }
    }
}
