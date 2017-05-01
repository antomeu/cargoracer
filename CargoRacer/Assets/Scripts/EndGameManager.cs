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
    public Text TextRestart;
    public LeaderBoardsManager LeaderBoardManager;
    public InputField InputFieldEmail;
    public ConclifyApi api;

	void Start ()
	{
        ButtonRestart.enabled = false;
        TextRestart.enabled = false;
        api.GameUpdated += HandleGameUpdated;
		HandleGameUpdated();
		api.RequestGameScoresGet();
	}
	


	public void SetPlayerScore()
	{
        if (InputFieldEmail.text != string.Empty)
        {
            api.RequestPlayerPatch(Globals.PlayerName, emailAddress: InputFieldEmail.text);
            api.RequestPlayerScorePost(Globals.PackagesDelivered);
            HandleGameUpdated();
            api.RequestGameScoresGet();
            ButtonRestart.enabled = true;
            TextRestart.enabled = true;
        }
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
