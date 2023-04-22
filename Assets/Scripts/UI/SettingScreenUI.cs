using UnityEngine;

public class SettingScreenUI : MonoBehaviour
{
    [SerializeField] private VolumeText musicVolumeText;
    [SerializeField] private VolumeText soundVolumeText;
    [SerializeField] private MainMenuSoundManager mainMenuSoundManager;
    public void SoundVolumeInMenu(){
        mainMenuSoundManager.ChangeSoundVolumeInMenu();
        soundVolumeText.DisplayVolume();
    }

    public void MusicVolumeInMenu(){
        mainMenuSoundManager.ChangeMusicVolumeInMenu();
        musicVolumeText.DisplayVolume();
    }
}
