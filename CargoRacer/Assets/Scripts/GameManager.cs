using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public AvatarController AvatarController;
    public UIController UIController;
    //public APIManager APIManager;
    public GameObject StartGamePanel;

    public Camera GameCamera;
    public Camera EndCamera;
    public GameObject GameUI;
    public Collider PlayerCollider;

	void Start () {
        Globals.Reset();
        StartGamePanel.SetActive(true);

    }

	void Update ()
	{
	    if (Globals.Lives <= 0 && Globals.GameState == GameState.Playing)
	    {
	        EndGame();
	    }

        if (Globals.PackagesDelivered >= Globals.LevelTreshold && Globals.Level == 1)
            Globals.Level = 2;
        if (Globals.PackagesDelivered >= Globals.LevelTreshold * 2 && Globals.Level == 2)
            Globals.Level = 3;

        if (Input.GetKeyDown(KeyCode.Escape)) // for PC testing
        {
            Restart();
        }
    }

    void EndGame()
    {

        //Globals.Speed = 0;
        Globals.GameState = GameState.End;

        UIController.EndGamePanel.SetActive(true);

        //TODO: Activate ending camera
        //TODO: Deactivate UI
        ToggleEndAnimation(false);
        
        //UIController.EndGamePanel.GetComponent<EndGameManager>().SetPlayerScore();
		

		//UIController.EndGameScore.text = "YOUR SCORE IS:\n" + Globals.PackagesDelivered.ToString() + "\n";



    }

    void ToggleEndAnimation(bool IsRestart)
    {
        EndCamera.gameObject.SetActive(!IsRestart); //false
        GameCamera.gameObject.SetActive(IsRestart); //true
        GameUI.SetActive(IsRestart); //true
        PlayerCollider.enabled = IsRestart;//true
    }


    public void Restart()
    {

        Globals.Reset();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        StartGamePanel.SetActive(true);
        ToggleEndAnimation(true);
    }
    void SendScore()
    {
        //APIManager.StartCoroutine("Start");
        //UIController.NetworkDebugData.text = APIManager.Response;
        
    }

    public void PauseGame(bool isNotPaused)
    {
        if (isNotPaused) // then pause game
            Globals.GameState = GameState.Paused;
        else // then unpause game
            Globals.GameState = GameState.Playing;
    }
}
