using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public AudioSource EngineLoop;
    public AudioSource Crash;
    public AudioSource PickUp;
    public AudioSource DropOff;
    public AudioSource FinalCrash;
    public AudioSource Boost;
    public AudioSource CannotPickUp;
    public AudioSource TurnSkid;

    public AudioSource[] Music;
    //public int Globals.MusicIndex = 0;

    public AudioListener AudioListener;

    public Text MusicNumber;

    public float NormalPitch = 0.44f;
    // Use this for initialization
    void Start () {
		Music[Globals.MusicIndex].Play();
	}
	
	// Update is called once per frame
	public void Update () {
        EngineLoop.pitch = NormalPitch * (Globals.Speed / Globals.NominalSpeed);
	}

    public void NextMusic()
    {
        Music[Globals.MusicIndex].Stop();
        Globals.MusicIndex = (Globals.MusicIndex < Music.Length-1) ? Globals.MusicIndex + 1 : 0;
        Music[Globals.MusicIndex].Play();
        MusicNumber.text =  (Globals.MusicIndex+1).ToString();
    }

    public void ToggleAudio(bool active)
    {
        AudioListener.gameObject.SetActive(active);
    }
}
