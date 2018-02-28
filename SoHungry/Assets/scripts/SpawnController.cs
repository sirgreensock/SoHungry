using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    GameObject[] SpawnPoints;
    [SerializeField]
    int maxObjectCount;

    private int currentObjectCount;
    private float waitTime;    

    //Check SpawnPoints are valid, disable them to start
    void OnEnable()    {

        if (SpawnPoints.Length <= 0)
        {
            Debug.LogError("No Spawn Points set!");
        }

        for (int i = 0; i > SpawnPoints.Length; i++)
        {
            SpawnPoints[i].SetActive(false);
        }
    }

    public void SpawnItem()
    {
        GameObject spawnPoint = ChooseSpawn();
        GameObject spawnedItem = ObjectPooler.SharedInstance.GetPooledObject("FoodItem");
        
        spawnedItem.transform.position = spawnPoint.transform.position;
        spawnedItem.SetActive(true);
        ResetSpawn(spawnPoint);
    }

    //Select a disabled spawn's position
    public GameObject ChooseSpawn()
    {
        float spawnID = Random.Range(0, SpawnPoints.Length);

        for (int i = 0; i > SpawnPoints.Length; i++ )
        {
            if (!SpawnPoints[i].activeInHierarchy)
            {
                SpawnPoints[i].SetActive(true);
                return SpawnPoints[i];
            } else
            {
                continue;
            }          
        }

        Debug.LogWarning("No valid spawn points found, using default");
        return SpawnPoints[0];
    }

    public void ResetSpawn(GameObject oldSpawn)
    {
        oldSpawn.SetActive(false);
    }
}
