using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectile;
    public AudioClip gunSound;
    public float range = 50.0f;
    public bool isLob = false;
    public bool isDumb = false;
    public float bulletImpulse = 10.0f;
    public int bulletsInMag = 1;
    public float reloadTime = 2.0f;
    private float timeBetweenShots = .05f;
    private Transform target;
    private int layerMask;
    

    private void Awake()
    {
        if (isDumb == false) {
            target = GameObject.FindWithTag("Player").transform;
        } else {
            target = transform.Find("Aimer");
        }        
        int enemyLayerIndex = LayerMask.NameToLayer("Enemies");
        int bulletLayerIndex = LayerMask.NameToLayer("Bullets");
        layerMask = (1 << enemyLayerIndex) | (1 << bulletLayerIndex);
        layerMask = ~layerMask;        
    }

    void Start()
    {           
        StartCoroutine("Fire", reloadTime);
    }

    void Update()
    {
    }

    IEnumerator Fire(float reload)
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            bool canFire = false;
            if(isDumb == false) {
                RaycastHit hit;               
                if (Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out hit, range, layerMask)) {
                    if (hit.collider.tag == target.tag) {
                        canFire = true;
                    }
                }
            }
            if (isDumb == true || canFire == true) {
                for (int i = 0; i < bulletsInMag; i++) {
                    GameObject temp = ObjectPool.SharedInstance.GetPooledObject(projectile.tag);
                    temp.transform.position = transform.position;
                    temp.transform.rotation = transform.rotation;
                    temp.SetActive(true);
                    Fire(temp);
                    yield return new WaitForSeconds(timeBetweenShots);
                }
            }                        
            yield return new WaitForSeconds(reload);
        }
    }

    public void Fire(GameObject obj)
    {
        if(isLob == true)
        {
            var dir = target.position - transform.position; // get target direction
            var h = dir.y;  // get height difference
            dir.y = 0;  // retain only the horizontal direction
            var dist = dir.magnitude;  // get horizontal distance
            dir.y = dist;  // set elevation to 45 degrees
            dist += h;  // correct for different heights
            var vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
            obj.transform.GetComponent<Rigidbody>().velocity = vel * dir.normalized;
        }
        else
        {
            obj.transform.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * bulletImpulse, ForceMode.Impulse);
        }
        AudioSource.PlayClipAtPoint(gunSound, transform.position);
    }
}


