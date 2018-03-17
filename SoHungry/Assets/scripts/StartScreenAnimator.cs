using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenAnimator : MonoBehaviour {

    [SerializeField]
    GameObject objectToActivate;

	// Use this for initialization
	void Start () {
        objectToActivate.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void AnimOver()
    {
        objectToActivate.SetActive(true);
    }
}
