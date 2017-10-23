using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Conclify;
using Conclify.Game;

public class SettingLeaderboardManager : MonoBehaviour
{

    public LeaderBoardsManager LeaderBoardManager;

    public ConclifyApi Api;


    void Start()
    {

        Api.GameUpdated += HandleGameUpdated;
        HandleGameUpdated();
        Api.PlayerUpdated += PlayerUpdated;
        Api.RequestGameScoresGet();
        Debug.Log("id" + Api.Player.Id + " | Name: " + Api.Player.FirstName + " | email: " + Api.Player.EmailAddress);
        

    }

    void PlayerUpdated()
    {
        Debug.Log("id" + Api.Player.Id + " | Name: " + Api.Player.FirstName + " | email: " + Api.Player.EmailAddress);
    }






    private void HandleGameUpdated()
	{

        if (!Api.Game.Scores.Any())
			return;

		int index = 0;
		foreach(ConclifyApiGameScore gameScore in Api.Game.Scores)
		{

            if (index < LeaderBoardManager.PlayerScoreManager.Length)// Only display the top of the board
			{
				LeaderBoardManager.PlayerScoreManager[index].SetPlayerInfo(gameScore.Rank, gameScore.Name, gameScore.Score);
			}
			index++;
		}

    }
}
