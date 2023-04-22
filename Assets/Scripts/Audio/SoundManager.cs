using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    // private AudioSource _audioSource;
    [SerializeField] protected AudioSource soundSource;
    [SerializeField] protected AudioSource musicSource;
    

     
    // Start is called before the first frame update
    public virtual void Start()
    {
        instance = this;
        ChangeMusicVolume(0);
        ChangeSoundVolume(0);
    }

    public void PlaySound(AudioClip _sound){
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change){
        // Get the initial volume and change it
        float currentVolume = PlayerPrefs.GetFloat("SoundVolume"); // Load last saved volume from player preferences
        currentVolume = currentVolume + _change;

        // Check if the volume reaches the minimum or maximum value
        if(Math.Round(currentVolume, 1) > 1){
            currentVolume = 0;
        }
        else if(currentVolume < 0){
            currentVolume = 1;
        }

        // Assign the final volume
        soundSource.volume = currentVolume;
        // Debug.Log("Sound volume: " + currentVolume);

        // Save the final volume to the player preferences
        PlayerPrefs.SetFloat("SoundVolume", soundSource.volume);
    }

    public void ChangeMusicVolume(float _change){
        // Define a base volume
        float baseVolume = 0.5f;
        // Get the initial volume and change it
        float currentVolume = PlayerPrefs.GetFloat("MusicVolume"); // Load last saved volume from player preferences
        currentVolume = currentVolume + _change;

        // Check if the volume reaches the minimum or maximum value
        if(Math.Round(currentVolume, 1) > 1){
            currentVolume = 0;
        }
        else if(currentVolume < 0){
            currentVolume = 1;
        }

        // Assign the final volume
        float finalVolume = currentVolume*baseVolume;
        musicSource.volume = finalVolume;
        // Debug.Log("Music volume: " + musicSource.volume);
        // Debug.Log("Current volume: " + currentVolume);


        // Save the final volume to the player preferences
        PlayerPrefs.SetFloat("MusicVolume", currentVolume);
    }
}
