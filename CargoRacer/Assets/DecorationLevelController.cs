using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationLevelController : MonoBehaviour {
    public GameObject[] DecorationForLevel = new GameObject[Globals.TotalLevels];

    public void Start()
    {
        SwitchDecorationForLevelChange(Globals.Level);
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
        if (level == 4 && transform.position.x > 0) //move big buildings on the right slightly away
            DecorationForLevel[level-1].transform.localPosition = new Vector3(-15, 0, 0);

    }
}
