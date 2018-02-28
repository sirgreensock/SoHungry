using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{    
    public GameObject objectToPool;
    public int amountToPool;
}

public class ObjectPooler : MonoBehaviour {

    public List<ObjectPoolItem> itemsToPool;
    public List<GameObject> pooledObjects;
    public static ObjectPooler SharedInstance;

    void Awake() {
        SharedInstance = this;
    }    

	// Use this for initialization
	void Start () {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}

/*
GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("TagName"); 
  if (bullet != null) {
    bullet.transform.position = turret.transform.position;
    bullet.transform.rotation = turret.transform.rotation;
    bullet.SetActive(true);
  }

    in pooled object  use OnEnable() instead of Start()
  */