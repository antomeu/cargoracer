using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardsManager : MonoBehaviour {

    #region Set in Unity
    public PlayerScoreManager[] PlayerScoreManager; 
    #endregion
    


    void SetPlayersInfo(string rank, string name, string score)
    {
        foreach(PlayerScoreManager playerEntry in PlayerScoreManager)
        {
            playerEntry.SetPlayerInfo(rank,name,score);
        }
    }
}
