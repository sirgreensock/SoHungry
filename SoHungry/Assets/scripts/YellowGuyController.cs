using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowGuyController : MonoBehaviour {

    public GameManager gameManager;
   

	// Use this for initialization
	void Start () {
        gameManager.GameStartBool = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GameStart()
    {
        gameManager.GameStartBool = true;
        gameManager.StartGame();        
    }

    void GameEnd ()
    {
        gameManager.ShowResultScreen();
    }
}
