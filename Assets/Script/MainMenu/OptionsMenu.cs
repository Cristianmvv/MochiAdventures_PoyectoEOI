using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
   public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void ChangeVolumeMusic(float volume)
    {
        audioMixer.SetFloat("VolumeMusic", volume);
    }
    public void ChangeVolumeSfx(float volume)
    {
        audioMixer.SetFloat("VolumeSfx", volume);
    }
}
