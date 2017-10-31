using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Conclify;
using Conclify.Game;

public class EndGameManager : MonoBehaviour
{
    public Text TextRestart;
    public LeaderBoardsManager LeaderBoardManager;
    public InputField InputFieldEmail;
    public Button SubmitScoreButton;
    public ConclifyApi Api;
    public GameObject DisclaimerButton;
    public GameObject EmailPanel;
    public GameObject SubmitButton;

    void Start()
    {
        EmailPanel.SetActive(true);
        DisclaimerButton.SetActive(false);
        SubmitButton.SetActive(false);

        Api.GameUpdated += HandleGameUpdated;
        HandleGameUpdated();
        Api.PlayerUpdated += PlayerUpdated;
        Api.RequestGameScoresGet();
        Debug.Log("id" + Api.Player.Id + " | Name: " + Api.Player.FirstName + " | email: " + Api.Player.EmailAddress);

        if (!string.IsNullOrEmpty(Api.Player.EmailAddress) && Api.IsValidEmail(Api.Player.EmailAddress)) // check that api has an email for this user and that it is valid
        {
            //SetPlayerScore(); //send score
            InputFieldEmail.text = Api.Player.EmailAddress; //Show email
            SubmitScoreButton.gameObject.SetActive(true); //Activate submit button
        }
        else
            SubmitScoreButton.gameObject.SetActive(false); //Deactivate submit button
    }

    void PlayerUpdated()
    {
        Debug.Log("id" + Api.Player.Id + " | Name: " + Api.Player.FirstName + " | email: " + Api.Player.EmailAddress);
    }


    public void SetPlayerScore()
    {
        SubmitScoreButton.gameObject.SetActive(false);
        Api.RequestPlayerPatch(Globals.PlayerName, emailAddress: InputFieldEmail.textComponent.text);
        Api.RequestPlayerScorePost(Globals.PackagesDelivered);
        HandleGameUpdated();
        Api.RequestGameScoresGet();
    }

    public void CheckEmailValidity()
    {
        if (InputFieldEmail.textComponent.text != string.Empty &&  Api.IsValidEmail(InputFieldEmail.textComponent.text)) //if input is not empty, or email already exists AND email is valid (both in input and API)
            SubmitScoreButton.gameObject.SetActive( true);
        else //if (InputFieldEmail.textComponent.text != string.Empty && Api.IsValidEmail(InputFieldEmail.textComponent.text))
            InputFieldEmail.text = "Please enter a valid e-mail address";
        
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
