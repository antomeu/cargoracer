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
    public AudioSource TurnSkid;

    public AudioSource[] Music;
    public int MusicIndex = 0;

    public AudioListener AudioListener;

    public float NormalPitch = 0.44f;
    // Use this for initialization
    void Start () {
		Music[MusicIndex].Play();
	}
	
	// Update is called once per frame
	public void Update () {
        EngineLoop.pitch = NormalPitch * (Globals.Speed / Globals.NominalSpeed);
	}

    public void NextMusic()
    {
        Music[MusicIndex].Stop();
        MusicIndex = (MusicIndex < Music.Length-1) ? MusicIndex + 1 : 0;
        Music[MusicIndex].Play();
    }

    public void ToggleAudio(bool active)
    {
        AudioListener.gameObject.SetActive(active);
    }
}
