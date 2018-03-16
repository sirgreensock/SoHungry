using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour {
    [SerializeField]
    TextMeshProUGUI timerText;

    private bool gameStart;
    public bool GameStartBool
    {
        get { return gameStart; }
        set { gameStart = value; }
    }
    private bool gamePaused;
    private int Timer = 30;
    private SpawnController spawnController;


	void Start () {
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        spawnController = gameObject.GetComponent<SpawnController>();
        gameStart = false;
        timerText.text = Timer.ToString();
	}
	
	void Update () {
		if (gameStart)
        {
            //Do game stuff            
        } else
        {
            //Do other stuff
        }

        if (gamePaused)
        {
            PauseGame();
        }
       
	}

    private void InitGame()
    {
        //Set up game init components
    }

    public void StartGame()
    {
        //Begin game logic
        spawnController.SpawningAllowed = true;
        gameStart = true;
        StartCoroutine("TimeCount");
    }

    private void TimeControl()
    {
        //keep track of timer and timescale
    }

    IEnumerator TimeCount()
    {
        while (gameStart)
        {
            yield return new WaitForSeconds(1);
            Timer--;
            timerText.text = Timer.ToString();

            if (Timer == 0)
            {
                gameStart = false;
                EndGame();
                break;
            }
        }
             
    }

    public void EndGame()
    {
        //do game debrief stuff
        Debug.Log("Hey game ended!");
        spawnController.SpawningAllowed = false;
        gameStart = false;
        StopCoroutine("TimeCount");
    }

    private void PauseGame()
    {
        //do pause game stuff
    }

    private void ResetGame()
    {
        //Start game over again
    }
}
