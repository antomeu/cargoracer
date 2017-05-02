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
    public ConclifyApi Api;

	void Start ()
	{
        ButtonRestart.enabled = false;
        TextRestart.enabled = false;
        Api.GameUpdated += HandleGameUpdated;
        HandleGameUpdated();
        Api.PlayerUpdated += PlayerUpdated;
        Api.RequestGameScoresGet();
        Debug.Log("id" + Api.Player.Id + " | Name: " + Api.Player.FirstName + " | email: " + Api.Player.EmailAddress);
        SetPlayerScore();
    }

    void PlayerUpdated()
    {
        Debug.Log("id" + Api.Player.Id + " | Name: " + Api.Player.FirstName + " | email: " + Api.Player.EmailAddress);
    }


    public void SetPlayerScore()
	{
        Debug.Log("Starting SetPlayerScore");
        if (InputFieldEmail.textComponent.text != string.Empty || !string.IsNullOrEmpty(Api.Player.EmailAddress))
        {
            //InputFieldEmail.textComponent.text = Api.Player.EmailAddress; // if player wants to resubmit
            InputFieldEmail.gameObject.SetActive(false);

            Debug.Log("Api.RequestPlayerPatch");

            Api.RequestPlayerPatch(Globals.PlayerName, emailAddress: InputFieldEmail.textComponent.text);

            Debug.Log("Api.RequestPlayerScorePost");

            Api.RequestPlayerScorePost(Globals.PackagesDelivered);

            Debug.Log("HandleGameUpdated");

            HandleGameUpdated();

            Debug.Log("Api.RequestGameScoresGet");

            Api.RequestGameScoresGet();
            ButtonRestart.enabled = true;
            TextRestart.enabled = true;
        }

        Debug.Log("Finishing SetPlayerScore");

    }

    private void HandleGameUpdated()
	{
        Debug.Log("Starting HandleGameUpdated");

        if (!Api.Game.Scores.Any())
			return;

		int index = 0;
		foreach(ConclifyApiGameScore gameScore in Api.Game.Scores)
		{
            Debug.Log("foreach score in api");

            if (index < LeaderBoardManager.PlayerScoreManager.Length)// Only display the top of the board
			{
				LeaderBoardManager.PlayerScoreManager[index].SetPlayerInfo(gameScore.Rank, gameScore.Name, gameScore.Score);
			}
			index++;
		}

        Debug.Log("Finishing HandleGameUpdated");

    }
}
