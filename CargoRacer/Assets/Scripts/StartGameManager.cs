using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameManager : MonoBehaviour {
    public GameObject MovingWorld;
    public Text TextPlayerName;
	
	void Start () {
        MovingWorld.SetActive(false);
	}
	
    public void StartGame()
    {
        if (true || TextPlayerName.text != string.Empty)//Temporary
        {
            Globals.PlayerName = TextPlayerName.text;
            MovingWorld.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
