﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsScreen : MonoBehaviour {
    [Header ("Screen Objects")]
    [SerializeField]
    Animator winTransition;

    [SerializeField]
    GameObject loseTransition;

    [SerializeField]
    Animator resultWindow;

    [SerializeField]
    Image mostEatenFood;

    [SerializeField]
    Image moodIndicator;

    [SerializeField]
    TextMeshProUGUI resultMoodText;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI foodsEatenText;

    [SerializeField]
    TextMeshProUGUI foodsRejectedText;

    [SerializeField]
    Sprite[] moodSprites;

    [SerializeField]
    string[] resultMoodString;

    [SerializeField]
    private List<FoodData> foodDataList;

    private ScoreController parentScoreController;   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetScoreController(ScoreController scoreController)
    {
        parentScoreController = scoreController;
    }

    public void HandleResultScreen(bool winState, float endScore)
    {
        if (winState)
        {
            winTransition.SetTrigger("Close");
            resultWindow.SetTrigger("Good");
        } else
        {
            loseTransition.SetActive(true);
            resultWindow.SetTrigger("Bad");
        }

        SetMood(endScore);
        SetMostEaten();        

        int goodEaten = parentScoreController.GoodEats;
        int badEaten = parentScoreController.BadEats;

        float score = ((endScore - 0.5f) * (goodEaten - badEaten) ) * 100f;
        scoreText.text = score.ToString();

        foodsEatenText.text = goodEaten.ToString();
        foodsRejectedText.text = badEaten.ToString();
        
    }

    void SetMood(float endScore)
    {        

        if (endScore < 8f)
        {
            //set sad face
            moodIndicator.sprite = moodSprites[2];
            resultMoodText.text = resultMoodString[2];
        }
        if (endScore > 8f && endScore < 15f)
        {
            //set neutral face
            moodIndicator.sprite = moodSprites[1];
            resultMoodText.text = resultMoodString[1];
        }
        if (endScore > 15f)
        {
            //set happy face
            moodIndicator.sprite = moodSprites[0];
            resultMoodText.text = resultMoodString[0];
        }
    }

    void SetMostEaten()
    {
        
        foodDataList.Sort(delegate (FoodData a, FoodData b)
        {
            return (a.foodEaten.CompareTo(b.foodEaten));
        });

        mostEatenFood.sprite = foodDataList[6].foodSprite;
    }


}
