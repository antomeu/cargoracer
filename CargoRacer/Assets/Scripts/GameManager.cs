using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public AvatarController AvatarController;
    public UIController UIController;
    //public APIManager APIManager;
    public GameObject StartGamePanel;

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

        if (Globals.PackagesDelivered >= Globals.LevelTreshold)
            Globals.Level = 2;
        if (Globals.PackagesDelivered >= Globals.LevelTreshold * 2 )
            Globals.Level = 3;
        
    }

    void EndGame()
    {

        Globals.Speed = 0;
        Globals.GameState = GameState.End;

        UIController.EndGamePanel.SetActive(true);
        
        //TODO: Activate ending camera
        //TODO: Deactivate UI
        
        
        //UIController.EndGamePanel.GetComponent<EndGameManager>().SetPlayerScore();
		

		//UIController.EndGameScore.text = "YOUR SCORE IS:\n" + Globals.PackagesDelivered.ToString() + "\n";



        if (Input.GetKeyDown(KeyCode.Escape)) // for PC testing
        {
            Restart();
        }
    }
    public void Restart()
    {

        Globals.Reset();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        StartGamePanel.SetActive(true);
    }
    void SendScore()
    {
        //APIManager.StartCoroutine("Start");
        //UIController.NetworkDebugData.text = APIManager.Response;
        
    }
}
