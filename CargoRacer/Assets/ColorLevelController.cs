using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLevelController : MonoBehaviour {
    public Color[] LevelColor = new Color[Globals.TotalLevels];
    public Material Material;
	void Update () {
        Material.SetColor("_Color", LevelColor[Globals.Level-1]);
	}
}
