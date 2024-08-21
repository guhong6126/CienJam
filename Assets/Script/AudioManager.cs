using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource clickAudio;
    public AudioSource ErrorAudio;
    public AudioSource officeSound;
    public AudioSource alarmSound;

    public Slider BgmControl;
    public Slider SfxControl;


    private void Update()
    {
        clickAudio.volume = SfxControl.value;
        ErrorAudio.volume = SfxControl.value;
        officeSound.volume = BgmControl.value;
        alarmSound.volume = SfxControl.value;
    }
}
