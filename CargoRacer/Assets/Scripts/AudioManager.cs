using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource EngineLoop;
    public AudioSource Crash;
    public AudioSource PickUp;
    public AudioSource DropOff;
    public AudioSource FinalCrash;
    public AudioSource Boost;
    public AudioSource CannotPickUp;

    public float NormalPitch = 0.44f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        EngineLoop.pitch = NormalPitch * (Globals.Speed / Globals.NominalSpeed);
	}
}
