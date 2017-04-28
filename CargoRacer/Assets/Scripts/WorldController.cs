using UnityEngine;

public class WorldController : MonoBehaviour {

    #region Set in Unity

    public float ClippingDistance = 400;
    public float Speed;
    public float TrafficSpeed = 10;
    public Transform OppositeTraffic;
    public Transform SameWayTraffic;
    public GameObject[] Vehicles;
    public GameObject[] Houses;
    public GameObject[] Trees;
    public GameObject[] Props;
    public GameObject[] Roads;
        
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
        
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
