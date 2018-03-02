using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour {
    private bool gameStart;
    private bool gamePaused;
    private int Timer;


	void Start () {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
	}
	
	void Update () {
		if (gameStart)
        {
            //Do game stuff
        } else
        {
            //Do other stuff
        }
       
	}

    private void InitGame()
    {
        //Set up game init components
    }

    private void StartGame()
    {
        //Begin game logic
    }

    private void TimeControl()
    {
        //keep track of timer and timescale
    }

    private void EndGame()
    {
        //do game debrief stuff
    }

    private void ResetGame()
    {
        //Start game over again
    }
}
