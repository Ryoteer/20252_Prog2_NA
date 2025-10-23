using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    #region Instance
    public static AudioManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Parameters
    [Header("<color=orange>Audio</color>")]
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _masterGroupName = "Master";
    [Range(0.0001f, 1.0f)][SerializeField] private float _masterInitialVolume = 1.0f;
    public float MasterInitialVolume
    {
        get { return _masterInitialVolume; }
        private set { _masterInitialVolume = value; }
    }
    [SerializeField] private string _musicGroupName = "Music";
    [Range(0.0001f, 1.0f)][SerializeField] private float _musicInitialVolume = 0.5f;
    public float MusicInitialVolume
    {
        get { return _musicInitialVolume; }
        private set { _musicInitialVolume = value; }
    }
    [SerializeField] private string _sfxGroupName = "SFX";
    [Range(0.0001f, 1.0f)][SerializeField] private float _sfxInitialVolume = 0.75f;
    public float SFXInitialVolume
    {
        get { return _sfxInitialVolume; }
        private set { _sfxInitialVolume = value; }
    }
    [SerializeField] private string _uiGroupName = "UI";
    [Range(0.0001f, 1.0f)][SerializeField] private float _uiInitialVolume = 1.0f;
    public float UIInitialVolume
    {
        get { return _uiInitialVolume; }
        private set { _uiInitialVolume = value; }
    }

    private AudioSource _musicSource;
    #endregion

    private void Start()
    {
        _musicSource = GetComponent<AudioSource>();

        SetMasterVolume(_masterInitialVolume);
        SetMusicVolume(_musicInitialVolume);
        SetSFXVolume(_sfxInitialVolume);
        SetUIVolume(_uiInitialVolume);
    }

    public void SetMasterVolume(float value)
    {
        if (value <= 0.0f) value = 0.0001f;

        _mixer.SetFloat(_masterGroupName, Mathf.Log10(value) * 20.0f);
    }

    public void SetMusicVolume(float value)
    {
        if (value <= 0.0f) value = 0.0001f;

        _mixer.SetFloat(_musicGroupName, Mathf.Log10(value) * 20.0f);
    }

    public void SetSFXVolume(float value)
    {
        if (value <= 0.0f) value = 0.0001f;

        _mixer.SetFloat(_sfxGroupName, Mathf.Log10(value) * 20.0f);
    }

    public void SetUIVolume(float value)
    {
        if (value <= 0.0f) value = 0.0001f;

        _mixer.SetFloat(_uiGroupName, Mathf.Log10(value) * 20.0f);
    }

    public void PlayMusicClip(AudioClip clip)
    {
        if (_musicSource.clip && clip == _musicSource.clip) return;

        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }

        _musicSource.clip = clip;

        _musicSource.Play();
    }
}
