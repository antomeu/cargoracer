using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldController : MonoBehaviour {

    #region Set in Unity

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
	    transform.position += Speed * Time.deltaTime * Vector3.back;
        OppositeTraffic.position += TrafficSpeed * Time.deltaTime * Vector3.back;
        SameWayTraffic.position += TrafficSpeed * Time.deltaTime * Vector3.forward;
    }
}
