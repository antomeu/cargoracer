﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public AvatarController AvatarController;
    public UIController UIController;
    public APIManager APIManager;

	void Start () {
        Globals.Reset();
        
	}

	void Update ()
	{
	    if (Globals.Lives <= 0 && Globals.GameState == GameState.Playing)
	    {
	        EndGame();
	    }
	}

    void EndGame()
    {

        Globals.Speed = 0;
        Globals.GameState = GameState.End;
        UIController.EndGamePanel.SetActive(true);
        UIController.EndGameScore.text = "YOUR SCORE IS:\n" + Globals.PackagesDelivered.ToString() + "\n";
        SendScore();
        if (Input.GetKey(KeyCode.Space))
        {
            Globals.Reset();
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        
    }

    void SendScore()
    {
        APIManager.StartCoroutine("Start");
        //UIController.NetworkDebugData.text = APIManager.Response;
        
    }
}