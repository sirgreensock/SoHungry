using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [SerializeField]
    TextMeshProUGUI timerText;

    [SerializeField]
    GameObject resultsScreen;

    [SerializeField]
    GameObject startScreen;

    [SerializeField]
    Animator yellowCharacter;

    [SerializeField]
    ResultsScreen resultScreenComponent;

    private bool gameStart;
    public bool GameStartBool
    {
        get { return gameStart; }
        set { gameStart = value; }
    }
    private bool gamePaused;
    private int Timer = 30;
    private SpawnController spawnController;
    private bool winState;


    void Start () {
        startScreen.SetActive(true);
        DOTween.Init(false, true, LogBehaviour.ErrorsOnly);
        spawnController = gameObject.GetComponent<SpawnController>();
        gameStart = false;
        timerText.text = Timer.ToString();
        resultsScreen.SetActive(false);
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
        spawnController.SpawningAllowed = false;
        gameStart = false;
        StopCoroutine("TimeCount");

        float endScore = gameObject.GetComponent<ScoreController>().currentScore;

        if (endScore > 15f)
        {
            winState = true;
            yellowCharacter.SetTrigger("Win");
        }
        else
        {
            yellowCharacter.SetTrigger("Lose");
        }
    }

    public void ShowResultScreen()
    {
        HandleResultScreen();
    }

    void HandleResultScreen()
    {
        resultsScreen.SetActive(true);

        float endScore = gameObject.GetComponent<ScoreController>().currentScore;

        resultScreenComponent.SetScoreController(gameObject.GetComponent<ScoreController>());
        resultScreenComponent.HandleResultScreen(winState, endScore);
    }

    private void PauseGame()
    {
        //do pause game stuff
    }

    public void ResetGame()
    {
        //Start game over again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
