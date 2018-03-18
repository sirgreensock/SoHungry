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

    public AudioController mainAudioController;

    public AudioController characterAudioController;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
            

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
      //  characterAudioController.PlayAudioClip("sohungry");
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
            mainAudioController.PlayAudioClip("win");
        }
        else
        {
            yellowCharacter.SetTrigger("Lose");
            mainAudioController.PlayAudioClip("lose");
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
