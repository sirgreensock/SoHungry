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

    private int currentObjectCount = 0;
    private float waitTime = 0;    

    public int CurrentObjectCount
    {
        get { return currentObjectCount; }        
    }

    public float WaitTime
    {
        set { waitTime = value; }
    }

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

    //Spawn item at selected spawn
    public void SpawnItem()
    {
        int spawnID = Random.Range(0, SpawnPoints.Length);
        GameObject spawnPoint = ChooseSpawn();
        GameObject spawnedItem = ObjectPooler.SharedInstance.GetPooledObject("FoodItem");
        if (spawnedItem != null && currentObjectCount <= maxObjectCount)
        {
            spawnedItem.transform.position = spawnPoint.transform.position;
            spawnedItem.GetComponent<FoodController>().SpawnObject = spawnPoint;
            spawnedItem.SetActive(true);
            currentObjectCount++;
            //ResetSpawn(spawnPoint);
        } else
        {
            Debug.Log("Reached Limit of food items!");
        }       
    }

    //Select a disabled spawn's position
    public GameObject ChooseSpawn()
    {       
        int spawnID = Random.Range(0, SpawnPoints.Length);
        while (SpawnPoints[spawnID].activeInHierarchy)
        {
            spawnID = Random.Range(0, SpawnPoints.Length);
        }

        return SpawnPoints[spawnID];        
    }

    //Make spawn point available again
    public void ResetSpawn(GameObject oldSpawn)
    {
        oldSpawn.SetActive(false);
    }

    //Reset object to make room for new ones
    public void ResetItem(GameObject oldObject)
    {
        Debug.Log("I am vanquished!");
        currentObjectCount--;
        GameObject oldSpawnObject = oldObject.GetComponent<FoodController>().SpawnObject;
        oldObject.SetActive(false);
        ResetSpawn(oldSpawnObject);
    }
}
