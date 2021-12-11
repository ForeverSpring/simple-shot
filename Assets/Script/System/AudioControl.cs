using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : PresistentSingleton<AudioControl>
{
    public AudioSource mSePause;
    public AudioSource mSeBGM;
    public float mBGMVolume;
    public float mSEVolume;

    private void Start() {
        InitSettings();
    }
    public void InitSettings() {
        mBGMVolume = 1f;
        mSEVolume = 1f;
    }

    public void UpdateBGMVolume() {
        Slider SliderBGMVolume = GameObject.Find("SliderBGMVolume").GetComponent<Slider>();
        if (SliderBGMVolume == null) {
            Debug.Log("BGMVolume not found!");
        }
        else {
            mBGMVolume = SliderBGMVolume.value;
        }
    }
    public void UpdateSEVolume() {
        Slider SliderSEVolume = GameObject.Find("SliderSEVolume").GetComponent<Slider>();
        if (SliderSEVolume == null) {
            Debug.Log("SEVolume not found!");
        }
        else {
            mSEVolume = SliderSEVolume.value;
        }
    }
    public void UpdateSettings() {
        Debug.Log("Update Options");
        mSeBGM.volume = mBGMVolume;
        mSePause.volume = mSEVolume;
        GameObject.Find("SliderBGMVolume").GetComponent<Slider>().value = mBGMVolume;
        GameObject.Find("SliderSEVolume").GetComponent<Slider>().value = mSEVolume;
    }
    public void PlayPause() {
        mSePause.Play();
    }

    public void PlayBGM() {
        //TODO:use PlayOneShot() to play audio sources
        mSeBGM.Play();
    }

    public void StopBGM() {
        mSeBGM.Stop();
    }

    public void PauseBGM() {
        mSeBGM.Pause();
    }
}
