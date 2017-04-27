using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public AvatarController AvatarController;
    public UIController UIController;

	// Use this for initialization
	void Start () {
        Globals.Reset();
	}
	
	// Update is called once per frame
	void Update () {
		if (Globals.Lives <= 0)
        {
            Globals.Speed = 0;
            Globals.GameState = GameState.End;
            UIController.EndGamePanel.SetActive(true);
            if (Input.GetKey("jump"))
            {
                Globals.Reset();
                SceneManager.LoadScene(0,LoadSceneMode.Single);
            }
        }
	}
}
