using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    GameObject[] SpawnPoints; //Spawn points to choose from
    [SerializeField]
    int maxObjectCount; //Number of objects allowed on screen at once
    [SerializeField]
    float waitMinimum;
    [SerializeField]
    float waitMaximum;

    private bool spawningAllowed;
    public bool SpawningAllowed
    {
        get { return spawningAllowed; }
        set { spawningAllowed = value; }
    }
    
    private int currentObjectCount = 0; //Current number of objects on screen
    public int CurrentObjectCount
    {
        get { return currentObjectCount; }        
    }

    private int objectsToSpawn;

    private float waitTime = 0;  //wait time between spawns
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

    void Update()
    {

        if (spawningAllowed)
        {
            if (currentObjectCount < maxObjectCount && (objectsToSpawn + currentObjectCount) < maxObjectCount)
            {
                objectsToSpawn = maxObjectCount - currentObjectCount;

                Spawner(objectsToSpawn);                        
            } 
            
        } else
        {
            StopAllCoroutines();
            List<GameObject> objectPool = gameObject.GetComponent<ObjectPooler>().pooledObjects;
            for (int i = 0; i < objectPool.Count; i++)
            {
                objectPool[i].SetActive(false);
            }
        }
    }

    private void Spawner(int spawnCount)
    {             
            for (int i = 0; i < spawnCount; i++)
            {
                StartCoroutine("SpawnTimer");
            }            
    }

    //Spawn item after waiting X seconds
    IEnumerator SpawnTimer()
    {
        waitTime = Random.Range(waitMinimum, waitMaximum);

        yield return new WaitForSeconds(waitTime);
        SpawnItem();
        objectsToSpawn--;
        yield return null;     
    }

    //Spawn item at selected spawn
    public void SpawnItem()
    {
        int spawnID = Random.Range(0, SpawnPoints.Length);
        GameObject spawnPoint = ChooseSpawn();
        GameObject spawnedItem = ObjectPooler.SharedInstance.GetPooledObject("FoodItem");
        if (spawnedItem != null && currentObjectCount < maxObjectCount)
        {
            //set spawn controller in food item
            spawnedItem.GetComponent<FoodController>().SpawnController = gameObject;

            spawnedItem.transform.position = spawnPoint.transform.position;
            spawnedItem.GetComponent<FoodController>().SpawnObject = spawnPoint;
            spawnedItem.SetActive(true);
            spawnPoint.SetActive(true);
            currentObjectCount++;            
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
        currentObjectCount--;
        GameObject oldSpawnObject = oldObject.GetComponent<FoodController>().SpawnObject;
        oldObject.SetActive(false);
        ResetSpawn(oldSpawnObject);
    }
}
