using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

    public int expireTime = 5;
    

    void OnEnable(){    
        StartCoroutine(RemoveAfterSeconds(expireTime, gameObject));
    }

    private void Awake()
    {
        
    }
        
    void Update () {
       
    }

    void OnCollisionEnter(Collision col)
    {
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

    private void OnDisable()
    {        
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}
