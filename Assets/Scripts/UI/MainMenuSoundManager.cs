using UnityEngine.UI;
using UnityEngine;

public class MainMenuSoundManager : SoundManager
{
    [SerializeField] protected Slider soundVolumeSlider;
    [SerializeField] protected Slider musicVolumeSlider;

    public void ChangeSoundVolumeInMenu(){
        float currentVolume = PlayerPrefs.GetFloat("SoundVolumeInMenu");
        currentVolume = soundVolumeSlider.value;
        soundSource.volume = currentVolume;
        Debug.Log("Sound Slider Value: " + currentVolume);
        PlayerPrefs.SetFloat("SoundVolumeInMenu", currentVolume);
    }

    public void ChangeMusicVolumeInMenu(){
        float currentVolume = PlayerPrefs.GetFloat("MusicVolumeInMenu");
        currentVolume = musicVolumeSlider.value;
        musicSource.volume = currentVolume;
        // Debug.Log("Music Slider Value: " + currentVolume);
        PlayerPrefs.SetFloat("MusicVolumeInMenu", currentVolume);
    }

    public override void Start()
    {
        ChangeMusicVolumeInMenu();
        ChangeSoundVolumeInMenu();
    }
}
