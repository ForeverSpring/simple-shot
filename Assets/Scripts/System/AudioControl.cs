using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : PersistentSingleton<AudioControl> {
    [Header("UI AudioSources")]
    public AudioSource mSePause;
    public AudioSource mSeBGM;
    public AudioSource mSeButtonSwitch;
    public AudioSource mSeButtonOK;
    public AudioSource mSeButtonInvalid;
    [Header("BGM AudioClip")]
    public AudioClip mBGMClipStage1Midway;
    public AudioClip mBGMClipStage1Boss;
    //Game Audio Sources
    [Header("GamePlay AudioSources")]
    public AudioSource mFukaExtend;
    public AudioSource mGetItem;
    public AudioSource mBossRayShot;
    public AudioSource mBossTan01;
    public AudioSource mBossTan02;
    public AudioSource mBossTanKira;
    public AudioSource mBossTanWoo;
    public AudioSource mTanWarning;
    public AudioSource mBeShot;
    public AudioSource mUseBomb;
    public AudioSource mBeatBoss;
    public AudioSource mPowerUp1;
    public AudioSource mPowerUp2;
    public AudioSource mGetSpell;
    public AudioSource mLifeUp1;
    public AudioSource mEnemyDead;
    public AudioSource mBulletHurtEnemy;
    public AudioSource mPlayerShot;
    [Range(0f, 1f)] public float mBGMVolume;
    [Range(0f, 1f)] public float mSEVolume;

    private void Start() {
        InitSettings();
    }
    public void InitSettings() {
        mBGMVolume = 1f;
        mSEVolume = 1f;
    }
    public void UpdateSettings() {
#if UNITY_EDITOR
        Debug.Log("Update Options");
#endif
        mSeBGM.volume = mBGMVolume;
        mSePause.volume = mSEVolume;
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
    public void PlayStage1Midway() {
        mSeBGM.clip = mBGMClipStage1Midway;
        mSeBGM.volume = mBGMVolume;
        mSeBGM.Play();
    }
    public void PlayStage1Boss() {
        mSeBGM.clip = mBGMClipStage1Boss;
        mSeBGM.volume = mBGMVolume;
        mSeBGM.Play();
    }
    public void PlayButtonSwitch() {
        mSeButtonSwitch.PlayOneShot(mSeButtonSwitch.clip, mSEVolume);
    }
    public void PlayButtonOK() {
        mSeButtonOK.PlayOneShot(mSeButtonOK.clip, mSEVolume);
    }
    public void PlayButtonInvalid() {
        mSeButtonInvalid.PlayOneShot(mSeButtonInvalid.clip, mSEVolume);
    }
    public void PlayFukaExtend() {
        mFukaExtend.PlayOneShot(mFukaExtend.clip, mSEVolume);
    }
    public void PlayGetItem() {
        mGetItem.PlayOneShot(mGetItem.clip, mSEVolume);
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
    public void PlayBossTanKira() {
        mBossTanKira.PlayOneShot(mBossTanKira.clip, mSEVolume);
    }
    public void PlayBossTanWoo() {
        mBossTanWoo.PlayOneShot(mBossTanWoo.clip, mSEVolume);
    }
    public void PlayTanWarning() {
        mTanWarning.PlayOneShot(mTanWarning.clip, mSEVolume);
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
    public void PlayEnemyDead() {
        mEnemyDead.PlayOneShot(mEnemyDead.clip, mSEVolume);
    }
    public void PlayLifeUp1() {
        mLifeUp1.PlayOneShot(mLifeUp1.clip, mSEVolume);
    }
    public void PlayGetSpell() {
        mGetSpell.PlayOneShot(mGetSpell.clip, mSEVolume);
    }
    public void PlayPowerUp1() {
        mPowerUp1.PlayOneShot(mPowerUp1.clip, mSEVolume);
    }
    public void PlayPowerUp2() {
        mPowerUp2.PlayOneShot(mPowerUp2.clip, mSEVolume);
    }
    public void PlayBulletHurtEnemy() {
        mBulletHurtEnemy.PlayOneShot(mBulletHurtEnemy.clip, mSEVolume);
    }
    public void PlayPlayerShot() {
        mPlayerShot.PlayOneShot(mPlayerShot.clip, mSEVolume);
    }
}
