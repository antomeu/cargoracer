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

    private bool PlayerExistsInApi;
	
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
            PlayerExistsInApi = false;
        }
        else //If player exists
        {
            InputFieldName.gameObject.SetActive(false);
            ForgetPlayerButton.gameObject.SetActive(true);
            StartButton.gameObject.SetActive(true);
            PlayerExistsInApi = true;
        }
    }
	
    public void StartGame()// triggered if start button is pressed (button in the background)
    {
        if (PlayerExistsInApi || !string.IsNullOrEmpty(TextEnteredPlayerName.text))
        {
            Globals.PlayerName = TextEnteredPlayerName.text;
            MovingWorld.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }

    public void ForgetPlayer()
    {
        Api.ForgetPlayer();
        InterfaceInit();
    }
}
