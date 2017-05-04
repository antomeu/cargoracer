using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationLevelController : MonoBehaviour {
    public GameObject[] DecorationForLevel = new GameObject[Globals.TotalLevels];

    public void Start()
    {
        DecorationForLevel[0].SetActive(true);
        DecorationForLevel[1].SetActive(false);
        DecorationForLevel[2].SetActive(false);
    }

    public void SwitchDecorationForLevelChange(int level)
    {
        for (int i = 0; i <= DecorationForLevel.Length-1; i++)
        {
            if (level-1 == i)
                DecorationForLevel[i].SetActive(true);
            else
                DecorationForLevel[i].SetActive(false);
        }
    }
}
