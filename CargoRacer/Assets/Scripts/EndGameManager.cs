using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Conclify;
using Conclify.Game;

public class EndGameManager : MonoBehaviour
{
    //public InputField InputName;
    public Button ButtonRestart; // Show when player name is submitter
    public LeaderBoardsManager LeaderBoardManager;
    public ConclifyApi api;

	void Start ()
	{
		api.GameUpdated += HandleGameUpdated;
		HandleGameUpdated();
		api.RequestGameScoresGet();
	}
	
    public void SendName()
    {
		api.RequestPlayerPatch(Globals.PlayerName);
    }

	public void SetPlayerScore()
	{
		api.RequestPlayerScorePost(Globals.PackagesDelivered);
		api.RequestGameScoresGet();
	}

	private void HandleGameUpdated()
	{
		if(!api.Game.Scores.Any())
			return;

		int index = 0;
		foreach(ConclifyApiGameScore gameScore in api.Game.Scores)
		{
			if(index < LeaderBoardManager.PlayerScoreManager.Length)// Only display the top of the board
			{
				LeaderBoardManager.PlayerScoreManager[index].SetPlayerInfo(gameScore.Rank, gameScore.Name, gameScore.Score);
			}
			index++;
		}
	}
}
