using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : PresistentSingleton<AudioControl> {
    public AudioSource mSePause;
    public AudioSource mSeBGM;
    public AudioSource mSeButtonSwitch;
    //Game Audio Sources
    public AudioSource mBossRayShot;
    public AudioSource mBossTan01;
    public AudioSource mBossTan02;
    public AudioSource mBeShot;
    public AudioSource mUseBomb;
    public AudioSource mBeatBoss;
    public float mBGMVolume;
    public float mSEVolume;

    private void Start() {
        InitSettings();
    }
    public void InitSettings() {
        mBGMVolume = 1f;
        mSEVolume = 1f;
    }
    //Volume set
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
    public void GetAudioSource() {
        mBossRayShot = GameObject.Find("ASRayShot").GetComponent<AudioSource>();
        mBossTan01 = GameObject.Find("ASTan01").GetComponent<AudioSource>();
        mBossTan02 = GameObject.Find("ASTan02").GetComponent<AudioSource>();
        mBeShot = GameObject.Find("ASBeShot").GetComponent<AudioSource>();
        mUseBomb = GameObject.Find("ASUseBomb").GetComponent<AudioSource>();
        mBeatBoss = GameObject.Find("ASBeatBoss").GetComponent<AudioSource>();
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
        mSeBGM.Play();
    }
    public void StopBGM() {
        mSeBGM.Stop();
    }
    public void PauseBGM() {
        mSeBGM.Pause();
    }
    public void PlayButtonSwitch() {
        mSeButtonSwitch.PlayOneShot(mSeButtonSwitch.clip, mSEVolume);
    }
    public void PlayBossRayShot() {
        mBossRayShot.PlayOneShot(mBossRayShot.clip, mSEVolume);
    }
    public void PlayBossTan01() {
        mBossTan01.PlayOneShot(mBossTan01.clip, mSEVolume);
    }
    public void PlayBossTan02() {
        mBossTan02.PlayOneShot(mBossTan02.clip, mSEVolume);
    }
    public void PlayBeShot() {
        mBeShot.PlayOneShot(mBeShot.clip, mSEVolume);
    }
    public void PlayUseBomb() {
        mUseBomb.PlayOneShot(mUseBomb.clip, mSEVolume);
    }
    public void PlayBeatBoss() {
        mBeatBoss.PlayOneShot(mBeatBoss.clip, mSEVolume);
    }

}
