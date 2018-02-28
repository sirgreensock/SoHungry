using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSorter : MonoBehaviour {

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float offsetY = gameObject.transform.position.y - 0.4f;
        Vector3 newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, offsetY);
        gameObject.transform.position = newPosition;

    }
}
