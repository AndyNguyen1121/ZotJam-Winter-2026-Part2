using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

[System.Serializable]
public class SFX{
    public AudioClip SFXClip;
    public string SFXName;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] SFX[] SFXFakeList;
    [SerializeField] AudioSource SFXFakeSource;
    static SFX[] SFXList;
    static AudioSource SFXSource;

    //[SerializeField] AudioClip soundEffectTest;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            SFXList = SFXFakeList;
            SFXSource = SFXFakeSource;
        }
    }

    public static void PlaySFX(string SFXTarget)
    {
        // find the sound in the array of sound
        SFX playedSound = Array.Find(SFXList, x => x.SFXName.Contains(SFXTarget) == true);

        if (playedSound == null)
        {
            // the sound doesn't exist in that instance of the AudioManager
            Debug.Log(SFXTarget + " is not a sound");
        }
        else
        {
            SFXSource.PlayOneShot(playedSound.SFXClip);
        }
    }
}

//public class AudioManager : MonoBehaviour
//{
//    public AudioSource SFXSource;
//    [SerializeField]
//    Sound[] soundsList; // array of sounds changed for each instance

//    // run this function in whatever script you want to play a sound
//    public void PlaySFX(string SFXtarget)
//    {
//        // find the sound in the array of sound
//        Sound playedSound = Array.Find(soundsList, x => x.SFXName.Contains(SFXtarget) == true);

//        if (playedSound == null)
//        {
//            // the sound doesn't exist in that instance of the AudioManager
//            Debug.Log(SFXtarget + " is not a sound");
//        }
//        else
//        {
//            SFXSource.volume = playedSound.SFXVolume;
//            SFXSource.PlayOneShot(playedSound.SFXClip);
//        }
//    }
//}
