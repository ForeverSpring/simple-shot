using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource SePause;
    public AudioSource SeBGM;

    public void PlayPause() {
        SePause.Play();
    }

    public void PlayBGM() {
        SeBGM.Play();
    }

    public void StopBGM() {
        SeBGM.Stop();
    }

    public void PauseBGM() {
        SeBGM.Pause();
    }
}
