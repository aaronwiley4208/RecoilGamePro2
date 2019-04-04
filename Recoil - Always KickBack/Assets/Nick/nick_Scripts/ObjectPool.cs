using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;    

    private void Awake()
    {
        SharedInstance = this;
    }
    // Use this for initialization
    void Start () {
        pooledObjects = new List<GameObject>();
        GrowPool();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        GrowPool();
        return GetPooledObject();
    }

    private void GrowPool()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
}
