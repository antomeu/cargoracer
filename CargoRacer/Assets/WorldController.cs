using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldController : MonoBehaviour {

    #region Set in Unity

    private float ClippingDistance;
    public float Speed;
    public float TrafficSpeed = 10;
    public Transform OppositeTraffic;
    public Transform SameWayTraffic;
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
        OppositeTraffic.position += TrafficSpeed * Time.deltaTime * Vector3.back;
        SameWayTraffic.position += TrafficSpeed * Time.deltaTime * Vector3.forward;
    }

    void SpawnDecorations()
    {

    }

    void SpawnRoad()
    {

    }

    void SpaweTraffic()
    {

    }
}
