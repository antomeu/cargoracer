using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreManager : MonoBehaviour {

    #region Set in Unity
    public Text TextRank;
    public Text TextName;
    public Text TextScore;
    public Image Medal;

    public Color[] MedalColors = new Color[5];
    
    #endregion

    public void SetPlayerInfo(string rank, string name, string score)
    {
        TextRank.text = rank;
        TextName.text = name;
        TextScore.text = score;
        int medalRank = 0;
        if (Int32.TryParse(rank[rank.Length - 1].ToString(), out medalRank))
        {
            Medal.color = MedalColors[medalRank - 1];
            Medal.enabled = true;
        }

        else
            Medal.enabled = false;


    }
}

