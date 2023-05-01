using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private CharacterAttack characterAttack;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private VolumeText musicVolumeText;
    [SerializeField] private VolumeText soundVolumeText;

    // Update is called once per frame
    void Update()
    {
        SetPauseScreen();
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    private void SetPauseScreen(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseScreen.activeInHierarchy){
                Resume();
            }
            else{
                PauseGame(true);
                Time.timeScale = 0;
                characterAttack.enabled = false;
                characterMovement.enabled = false;
                // audioSource.Stop();
            }
        }
    }

    private void PauseGame(bool _status){
        pauseScreen.SetActive(_status);
    }

    public void Resume(){
        Time.timeScale = 1;
        characterAttack.enabled = true;
        characterMovement.enabled = true;
        // audioSource.Play();
        PauseGame(false);
    }

    public void SoundVolume(){
        SoundManager.instance.ChangeSoundVolume(0.1f);
        soundVolumeText.UpdateVolume();
    }

    public void MusicVolume(){
        SoundManager.instance.ChangeMusicVolume(0.1f);
        musicVolumeText.UpdateVolume();
    }
}
