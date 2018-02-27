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
    Text scoreText;
    [SerializeField]
    Slider scoreSlider;

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

        if (scoreText !=null)
        {
            scoreText.text = gameScore.ToString();
        } else
        {
            Debug.LogWarning("No Score Text set!");
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
    }
    
    //Increase score
    public void GoodTarget(Collider2D heldObject)
    {
        gameScore++;
        gameObject.GetComponent<ObjectDragController>().DropItem();
        heldObject.gameObject.tag = "Untagged";
        heldObject.gameObject.SetActive(false);
        scoreText.text = gameScore.ToString();
        SetScoreSlider(gameScore);
    }

    //Reduce score
    public void BadTarget(Collider2D heldObject)
    {
        gameScore--;
        gameObject.GetComponent<ObjectDragController>().DropItem();
        heldObject.gameObject.tag = "Untagged";
        heldObject.gameObject.SetActive(false);
        scoreText.text = gameScore.ToString();
        SetScoreSlider(gameScore);
    }

    //tweens score value
    void SetScoreSlider(float newScore)
    {        
        newScore = Mathf.Clamp(newScore, sliderMin, 100f); //100f some big number, we only care about min here
        DOTween.To(()=> scoreTween, x => scoreTween = x, newScore, 0.5f);
               
    }
}


