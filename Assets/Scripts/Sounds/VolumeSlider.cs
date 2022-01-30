using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    FMOD.Studio.EventInstance VolumeTestEvent;
    
    //public FMODUnity.EventReference testRef;

    private FMOD.Studio.Bus MusicBus;
    private FMOD.Studio.Bus SFXBus;
    private FMOD.Studio.Bus MasterBus;

    //private float MusicVolume = 0.5f;
    private float SFXVolume = 0.5f;
    //private float MasterVolume = 1f;

    public string channel;
    [Range(0f, 1f)]
    public float defaultValue;

    private void Awake()
    {
        MusicBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music"); 
        SFXBus = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        //MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/Master"); 

        //VolumeTestEvent = FMODUnity.RuntimeManager.CreateInstance(testRef);
        LoadSettings();
    }

    private void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey(channel))
        {
            gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat(channel);
        }
        else
        {
            gameObject.GetComponent<Slider>().value = defaultValue;
            PlayerPrefs.SetFloat(channel, defaultValue);
            PlayerPrefs.Save();
        }       
    }


    //public void MasterVolumeLevel(float newMasterVolume)
    //{
    //    //MasterVolume = newMasterVolume;
    //    MasterBus.setVolume(newMasterVolume);
    //    SetVolumeLevel(channel, newMasterVolume);

    //}

    public void MusicVolumeLevel(float newMusicLevel)
    {
        //MusicVolume = newMusicLevel;
        MusicBus.setVolume(newMusicLevel);
        SetVolumeLevel(channel, newMusicLevel);
    }

    public void SFXVolumeLevel(float newSFXLevel)
    {
        //SFXVolume = newSFXLevel;
        SFXBus.setVolume(newSFXLevel);
        SetVolumeLevel(channel, newSFXLevel);
    }

    public void PlayVolumeLevel(float newLevel)
    {
        SFXVolume = newLevel;

        FMOD.Studio.PLAYBACK_STATE PbState;
        VolumeTestEvent.getPlaybackState(out PbState);
        if (PbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            VolumeTestEvent.start();
            //FMODUnity.RuntimeManager.PlayOneShot(testRef);

        }
    }

    public void SetVolumeLevel(string channel, float value)
    {
        PlayerPrefs.SetFloat(channel, value);
        PlayerPrefs.Save();
    }

    public void GetVolumeLevel(string channel)
    {
        gameObject.GetComponent<Slider>().value = PlayerPrefs.GetFloat(channel);
    }
}
