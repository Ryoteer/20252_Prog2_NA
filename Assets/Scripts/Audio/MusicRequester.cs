using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRequester : MonoBehaviour
{
    [Header("<color=orange>Audio</color>")]
    [SerializeField] private AudioClip _musicClip;

    private void Start()
    {
        AudioManager.Instance.PlayMusicClip(_musicClip);
    }
}
