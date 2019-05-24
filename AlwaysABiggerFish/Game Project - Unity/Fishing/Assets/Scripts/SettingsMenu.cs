using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {

    public Slider master, bgm, sfx;
    private float masterF, bgmF, sfxF;
    public AudioMixer audioMixer;

    private void Start()
    {
        audioMixer.GetFloat("volumeMaster", out masterF);
        master.value = masterF;

        audioMixer.GetFloat("volumeMusic", out bgmF);
        bgm.value = bgmF;

        audioMixer.GetFloat("volumeSFX", out sfxF);
        sfx.value = sfxF;

    }

    public void SetMasterVolume (float volume)
	{
		
		audioMixer.SetFloat("volumeMaster", volume);
	}

	public void SetMusicVolume (float volume)
	{
		audioMixer.SetFloat("volumeMusic", volume);
	}

	public void SetSFXVolume (float volume)
	{
		audioMixer.SetFloat("volumeSFX", volume);
	}
}
