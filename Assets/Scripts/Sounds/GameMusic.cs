using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamMusic : MonoBehaviour
{
    public FMODUnity.EventReference mainMenuMusic;

    private FMOD.Studio.EventInstance instance;

    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(mainMenuMusic);
        instance.start();
    }

    public void StopMusic()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    private void OnDestroy()
    {
        StopMusic();
    }
}
