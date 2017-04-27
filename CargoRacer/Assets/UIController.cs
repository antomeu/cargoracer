using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    #region Set in Unity
    public Text DistanceText;
    public Text PackagesDeliveredText;
    public Image LivesFillImage;
    public Image[] PackageImages = new Image[3];
    public GameObject EndGamePanel;
    #endregion
    private float totalLives;

    // Use this for initialization
    void Start () {
        totalLives = Globals.Lives;
        UpdateLivesUI();
        EndGamePanel.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
	{
	    DistanceText.text = Mathf.CeilToInt(Globals.Distance / 10000f).ToString();
        PackagesDeliveredText.text = "DELIVERIES: " + Globals.PackagesDelivered.ToString();
        UpdateLivesUI();
        UpdatePackages();
    }

    public void UpdateUI()
    {
        PackagesDeliveredText.text = "DELIVERIES: " + Globals.PackagesDelivered.ToString();
        UpdateLivesUI();
    }

    public void UpdateLivesUI()
    {
        LivesFillImage.fillAmount = Globals.Lives / totalLives;
    }

    public void UpdatePackages()
    {
        for (int i = 0; i <= PackageImages.Length - 1;i++)
        {
            PackageImages[i].enabled = Globals.PackageSlotIsUsed[i];
        }

    
    }
}
