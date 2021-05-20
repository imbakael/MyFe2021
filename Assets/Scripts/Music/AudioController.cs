using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [SerializeField] private AudioSource audioSource = default;
    [SerializeField] private AudioClip bgm = default;
    [SerializeField] private AudioClip battle = default;

    public void Play(string clipName) {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + clipName);
        audioSource.PlayOneShot(clip);
    }

    public void PlayBgm() {
        audioSource.Stop();
        audioSource.PlayOneShot(bgm);
    }

    public void PlayBattle() {
        audioSource.Stop();
        audioSource.PlayOneShot(battle);
    }
}
