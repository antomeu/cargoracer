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


	void Start ()
    {
        ButtonRestart.enabled = false;
    }
	
    public void SendName()
    {
        ButtonRestart.enabled = true;
    }

    
    private ConclifyApi GetApi()
    {
        ConclifyApi api = GetComponent<ConclifyApi>();
        return api;
    }

    void GetPlayersScore()
    {
        ConclifyApi api = GetApi();

        api.RequestPlayerPost();
        api.RequestPlayerPatch(Globals.PlayerName);
        api.RequestPlayerScorePost(Globals.PackagesDelivered);

        Debug.Log(api.Player.Id);

        for (int i = 0; i<= LeaderBoardManager.PlayerScoreManager.Length;i++)
        {
            string rank = gameScore.Rank; //String so it can be things like "=2"
            string name = gameScore.Name;
            string score = gameScore.Score; //String because it includes formatting, like thousand-separators
        }
    }
}
