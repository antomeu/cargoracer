﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text NetworkDebugData;

    #region Set in Unity
    public Text DistanceText;
    public Text PackagesDeliveredText;
    
    public Image LivesFillImage;
    public Image[] PackageImages = new Image[3];
    public GameObject EndGamePanel;
    #endregion
    private float totalLives;

    void Start () {
        totalLives = Globals.Lives;
        UpdateLivesUI();
        EndGamePanel.SetActive(false);

    }
	
	void Update ()
	{
	    DistanceText.text = (Globals.Distance / 10000000f).ToString("F2") + "Km";
        PackagesDeliveredText.text = "DELIVERIES: " + Globals.PackagesDelivered.ToString() ;
        UpdateUI();
    }

    public void UpdateUI()
    {
        
        UpdateLivesUI();
        UpdatePackages();
    }

    public void UpdateLivesUI()
    {
        LivesFillImage.fillAmount = (float) Globals.Lives / (float) Globals.MaximumLives;
        
    }

    public void UpdatePackages()
    {
        for (int i = 0; i <= PackageImages.Length - 1;i++)
        {
            PackageImages[i].enabled = Globals.PackageSlotIsUsed[i];
        }

    
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    public void ToggleMusic()
    {

    }

    public void ToggleAudio()
    {
        
    }
    

    public void QuitApplication()
    {
        Application.Quit();
    }
}
