using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Conclify;
using Conclify.Game;

public class StartGameManager : MonoBehaviour {
    public GameObject MovingWorld;
    public Text TextEnteredPlayerName;
    public Text TextWelcomeMessage;
    public InputField InputFieldName;
    public Button ForgetPlayerButton;
    public Button StartButton;
    public ConclifyApi Api;

    private bool CanStartGame;
	
	void Start () {
        MovingWorld.SetActive(false);
        InterfaceInit();

    }

    public void InterfaceInit()
    {
        if (string.IsNullOrEmpty(Api.Player.Id))//If there is no player saved
        {
            InputFieldName.gameObject.SetActive(true);
            ForgetPlayerButton.gameObject.SetActive(false);
            StartButton.gameObject.SetActive(false);
            CanStartGame = false;
        }
        else //If player exists
        {
            //Debug.Log("id" + Api.Player.Id + " | Name: " + Api.Player.FirstName + " | email: " + Api.Player.EmailAddress);
            InputFieldName.gameObject.SetActive(false);
            ForgetPlayerButton.gameObject.SetActive(true);
            StartButton.gameObject.SetActive(true);
            CanStartGame = true;
            TextWelcomeMessage.text = "Welcome back " + Api.Player.FirstName + "!";
        }
    }
	
    public void StartGame(int level)// triggered if start button is pressed (button in the background)
    {
        if (CanStartGame)
        {
            MovingWorld.SetActive(true);
            transform.gameObject.SetActive(false);
            Globals.Level = level;
            //TODO: Set chosen difficulty here
        }
    }

    public void AllowStartGame()
    {
        if (!string.IsNullOrEmpty(TextEnteredPlayerName.text))
        {
            CanStartGame = true;
            InputFieldName.gameObject.SetActive(false);
            StartButton.gameObject.SetActive(true);
            Globals.PlayerName = TextEnteredPlayerName.text;
            TextWelcomeMessage.text = "Welcome " + Globals.PlayerName;
        }
    }

    public void ForgetPlayer()
    {
        Api.ForgetPlayer();
        InterfaceInit();
    }
}
