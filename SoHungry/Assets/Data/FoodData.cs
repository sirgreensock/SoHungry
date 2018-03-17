using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Data", menuName = "FoodData")]
public class FoodData : ScriptableObject {

    public int foodID;
    public string foodName;
    public string foodTag;
    public Sprite foodSprite;
    public Sprite foodOutline;
    public int foodEaten;

}
