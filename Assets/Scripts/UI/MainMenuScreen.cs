using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public void Quit(){
        Application.Quit(); // Quit function only works when game was built
    }

    public void Play(){
        SceneManager.LoadScene("Level1"); // The main menu usually has index 0, so this line loads the main menu scene
        Time.timeScale = 1;
    }
}
