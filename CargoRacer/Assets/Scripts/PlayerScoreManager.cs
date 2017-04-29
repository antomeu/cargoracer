using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour {

    #region Set in Unity
    public Text TextRank;
    public Text TextName;
    public Text TextScore;

    
    #endregion

    public void SetPlayerInfo(string rank, string name, string score)
    {
        TextRank.text = rank;
        TextName.text = name;
        TextScore.text = score;
    }


}
