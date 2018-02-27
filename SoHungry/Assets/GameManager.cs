using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {
    [Header("Initialization")]
    [SerializeField]
    GameObject[] SpawnPoints;

	// Use this for initialization
	void Start () {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        if (SpawnPoints.Length <= 0)
        {
            Debug.LogError("No Spawn Points set!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChooseSpawn()
    {
        float spawnID = Random.Range(0, SpawnPoints.Length);
        Debug.Log(spawnID);
    }
}
