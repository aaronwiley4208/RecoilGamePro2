using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
        
    void OnEnable(){    
        StartCoroutine(RemoveAfterSeconds(2, gameObject));
    }

    private void Start()
    {
        
    }
        
    void Update () {
       
    }

    void OnCollisionEnter(Collision col)
    {
        gameObject.SetActive(false);        
    }

    private void OnDisable()
    {        
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(2);
        obj.SetActive(false);
    }
}
