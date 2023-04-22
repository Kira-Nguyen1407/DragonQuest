using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenUI : MonoBehaviour
{
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject gameOverScreen;
    public void Restart(){
        playerRespawn.Respawn();
        // SceneManager.LoadScene("Level1");
        DeactivateGameOverScreen();
    }

    public void GotoMainMenu(){
        SceneManager.LoadScene("MainMenu"); // The main menu usually has index 0, so this line loads the main menu scene
    }

    public void Quit(){
        Application.Quit(); // Quit function only works when game was built
    }

    public void ActivateGameOverScreen(){
        Time.timeScale = 0;
        // gameObject.SetActive(true);
        gameOverScreen.SetActive(true);
        audioSource.Stop();
    }

    public void DeactivateGameOverScreen(){
        Time.timeScale = 1;
        audioSource.Play();
        // gameObject.SetActive(false);
        gameOverScreen.SetActive(false);
    }
}
