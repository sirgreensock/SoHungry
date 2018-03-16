using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FoodSetup
{
    public string foodName;
    public string foodTag;
    public Sprite foodSprite;
    public Sprite foodOutline;
  
}

public class FoodController : MonoBehaviour {
    [SerializeField]
    float foodLifetime = 3f;
    [SerializeField]
    float foodSpeed = .75f;
    [SerializeField]
    GameObject foodAnimator;
    [SerializeField]
    GameObject foodImage;
    [SerializeField]
    List<FoodSetup> foodOptions;    

    public GameObject FoodImage
    {
        get { return foodImage; }
        set { foodImage = value; }
    }

    private SpriteRenderer foodSpriteRenderer;
    private string foodID;
    private string spawnID;
    private int foodNumber;
    private GameObject spawnController;
    public GameObject SpawnController
    {
        set { spawnController = value; }
        get { return spawnController; }
    }


    private GameObject spawnObject;
    public GameObject SpawnObject
    {
        set { spawnObject = value; }
        get { return spawnObject; }
    }

    private bool enableMovement;
    private float startX;
    private float startY;
    private float startZ;

    void OnEnable()
    {
        enableMovement = false;
        ChooseFood();
        StartCoroutine("foodTimer");
    }

    void Update()
    {
        if (enableMovement)
        {
            gameObject.transform.Translate(0f, -1f * foodSpeed * Time.deltaTime,0f);
        }
        
    }


    private void ChooseFood()
    {
        
        foodNumber = Random.Range(0, foodOptions.Count);   
        foodImage.tag = foodOptions[foodNumber].foodTag;
        foodImage.name = foodOptions[foodNumber].foodName;
        foodSpriteRenderer = foodImage.GetComponent<SpriteRenderer>();
        foodSpriteRenderer.sprite = foodOptions[foodNumber].foodSprite;
    }

    public void GrabItem()
    {
        foodSpriteRenderer.sprite = foodOptions[foodNumber].foodOutline;
    }

    public void DropItem()
    {
        foodSpriteRenderer.sprite = foodOptions[foodNumber].foodSprite;
    }

    IEnumerator foodTimer()
    {
        yield return new WaitForSeconds(foodLifetime);

        enableMovement = true;
        

        yield return null;
    }

    private void ResetSelf()
    {
        enableMovement = false;
        spawnController.GetComponent<SpawnController>().ResetItem(gameObject);
    }
}
