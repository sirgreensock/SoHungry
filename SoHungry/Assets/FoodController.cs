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
    GameObject foodAnimator;
    [SerializeField]
    GameObject foodImage;
    [SerializeField]
    List<FoodSetup> foodOptions;

    private SpriteRenderer foodSpriteRenderer;
    private string foodID;
    private string spawnID;
    private int foodNumber;
    private GameObject spawnController;

    private GameObject spawnObject;
    public GameObject SpawnObject
    {
        set { spawnObject = value; }
        get { return spawnObject; }
    }

    void OnEnable()
    {
        Debug.Log("Im Enabled");
        ChooseFood();
    }

	void Update () {
		
	}

    private void ChooseFood()
    {
        
        foodNumber = Random.Range(0, foodOptions.Count);        
        foodImage.tag = foodOptions[foodNumber].foodTag;
        Debug.Log(foodImage.tag);
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
}
