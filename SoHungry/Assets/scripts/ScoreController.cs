using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreController : MonoBehaviour {

    [Header("Initialization")]
    [SerializeField]
    GameObject mouthCollider;
    [SerializeField]
    Slider scoreSlider;
    [SerializeField]
    Image sliderFace;
    [SerializeField]
    Sprite sadFace;
    [SerializeField]
    Sprite neutralFace;
    [SerializeField]
    Sprite happyFace;

    private GameObject heldObject; //object that dropped in mouth
    private float sliderMin = 1.5f; //1.5f = minimum value that looks nice on slider
    private float gameScore = 1.5f; //starting score
    private float scoreTween; //tween value for slider

    public float currentScore        
    {
        get { return gameScore; }
        set
        {
            gameScore = value;
        }
    }
    
    void Start () {             

        if (mouthCollider != null)
        {            
            MouthController setMouthCollider = mouthCollider.GetComponent<MouthController>();
            setMouthCollider.ManagerObject = gameObject;
        } else
        {
            Debug.LogWarning("No Mouth Collider set!");
        }

        if (scoreSlider != null)
        {
            scoreTween = gameScore;
        } else
        {
            Debug.LogWarning("No Score Slider set!");
        }
    }
	
	// Update is called once per frame
	void Update () {
        scoreSlider.value = scoreTween;
        HandleSliderFace();
    }
    
    //Increase score, set item to reset
    public void GoodTarget(Collider2D heldObject)
    {
        gameScore++;
        gameObject.GetComponent<ObjectDragController>().DropItem();
        heldObject.gameObject.tag = "Untagged";
        gameObject.GetComponent<SpawnController>().ResetItem(heldObject.gameObject.transform.parent.parent.gameObject);
        SetScoreSlider(gameScore);
    }

    //Reduce score, set item to reset
    public void BadTarget(Collider2D heldObject)
    {
        gameScore--;
        gameObject.GetComponent<ObjectDragController>().DropItem();
        heldObject.gameObject.tag = "Untagged";
        gameObject.GetComponent<SpawnController>().ResetItem(heldObject.gameObject.transform.parent.parent.gameObject);
        SetScoreSlider(gameScore);
    }

    //tweens score value
    void SetScoreSlider(float newScore)
    {        
        newScore = Mathf.Clamp(newScore, sliderMin, 100f); //100f some big number, we only care about min here
        DOTween.To(()=> scoreTween, x => scoreTween = x, newScore, 0.5f);
               
    }

    private void HandleSliderFace()
    {
        if (gameScore < 8f)
        {
            //set sad face
            sliderFace.sprite = sadFace;
        }
        if (gameScore > 8f && gameScore < 15f)
        {
            //set neutral face
            sliderFace.sprite = neutralFace;
        }
        if (gameScore > 15f)
        {
            //set happy face
            sliderFace.sprite = happyFace;
        }
    }
}


