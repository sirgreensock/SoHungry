using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler SharedInstance;

    void Awake() {
        SharedInstance = this;
    }

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
