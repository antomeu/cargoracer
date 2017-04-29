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
        if (TextPlayerName.text != string.Empty)
        {
            Globals.PlayerName = TextPlayerName.text;
            MovingWorld.SetActive(true);
            transform.gameObject.SetActive(false);
        }
    }
}
