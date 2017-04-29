using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Conclify;
using Conclify.Game;
using TinyJSON;

public class EndGameManager : MonoBehaviour {
    //public InputField InputName;
    public Button ButtonRestart; // Show when player name is submitter
    public LeaderBoardsManager LeaderBoardManager;
    public ConclifyApi api;

	void Start ()
    {
        //ButtonRestart.enabled = false;
        GetPlayersScore();
    }
	
    public void SendName()
    {
        //ButtonRestart.enabled = true;
    }
    

    void GetPlayersScore()
    {
        
        api.RequestPlayerPost();
        api.RequestPlayerPatch(Globals.PlayerName);
        api.RequestPlayerScorePost(Globals.PackagesDelivered);

        Debug.Log(api.Player.FirstName);

        int index = 1;
        foreach (ConclifyApiGameScore gameScore in api.Game.Scores)
        {
            string rank = gameScore.Rank; 
            string name = gameScore.Name;
            string score = gameScore.Score;
            Debug.Log(gameScore);
            if (index <= LeaderBoardManager.PlayerScoreManager.Length)// Only display the top of the board
            {
                LeaderBoardManager.PlayerScoreManager[index].SetPlayerInfo(rank, name, score);
            }
            index++;

        }
    }
}
