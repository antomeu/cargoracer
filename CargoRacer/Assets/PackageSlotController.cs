using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageSlotController : MonoBehaviour {
    public bool SlotAvailable;

    public GameObject[] Package = new GameObject[3];

    private void Start()
    {
        DeactivatePackages();
    }

    void Update () {
		
	}

    //Write method to 
    public void ActivatePackage(PackageSlotController packageSlot, GameObject PickedUpPackage)
    {
        SlotAvailable = false;
        Debug.Log(PickedUpPackage.transform.parent.name[PickedUpPackage.transform.parent.name.Length - 1] -1 );
        packageSlot.Package[Int32.Parse(PickedUpPackage.transform.parent.name[PickedUpPackage.transform.parent.name.Length - 1].ToString()) - 1 ].SetActive(true);
    }

    public void DeactivatePackages()
    {
        //Should disable all packages in this slot
        SlotAvailable = true;
        
        for (int i = 0; i <= Package.Length-1; i++)
        {
            Package[i].SetActive(false);
        }

        
    }
}
