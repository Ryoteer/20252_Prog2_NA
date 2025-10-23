using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderBehaviour : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _uiSlider;

    private void Start()
    {
        _masterSlider.value = AudioManager.Instance.MasterInitialVolume;
        _musicSlider.value = AudioManager.Instance.MusicInitialVolume;
        _sfxSlider.value = AudioManager.Instance.SFXInitialVolume;
        _uiSlider.value = AudioManager.Instance.UIInitialVolume;
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }

    public void SetUIVolume(float value)
    {
        AudioManager.Instance.SetUIVolume(value);
    }
}
