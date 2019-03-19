using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    private static bool start = true;

    public AudioSource soundSource;
    public AudioSource musicSource;

    void Awake() {
        if (start) {
            start = false;

            DontDestroyOnLoad(this.gameObject);

            this.LoadSettings();

            this.musicSource.Play();
        }
    }

    public void PlaySound(AudioClip clip, float scale = 1f) {
        this.soundSource.PlayOneShot(clip);
    }

    public void SetSoundVolume(float vol) {
        PlayerPrefs.SetFloat("soundVolume", vol);
        this.LoadSettings();
    }

    public void SetMusicVolume(float vol) {
        PlayerPrefs.SetFloat("musicVolume", vol);
        this.LoadSettings();
    }

    public float GetSoundVolume() {
        return this.soundSource.volume;
    }

    public float GetMusicVolume() {
        return this.musicSource.volume;
    }

    private void LoadSettings() {
        this.soundSource.volume = PlayerPrefs.GetFloat("soundVolume", 1);
        this.musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 1);
    }

}
