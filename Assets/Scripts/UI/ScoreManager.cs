using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text keysCounterText;
    [SerializeField] private Text SBsCounterText; // Secret books
    private int currentScore;
    private int secretBookCounter;
    private int keyCounter;
    private int totalKeys;
    private int totalSecretBooks;

    private void Start(){
        currentScore = 0;
        secretBookCounter = 0;
        keyCounter = 0;
        totalKeys = 1;
        totalSecretBooks = 3;
    }

    public void UpdateScore(int _score){
        currentScore = currentScore + _score;
        scoreText.text = currentScore.ToString();
    }

    public void UpdateSecretBookCounter(){
        secretBookCounter = secretBookCounter + 1;
        SBsCounterText.text = secretBookCounter.ToString() + "/" + totalSecretBooks.ToString();
    }

    public void UpdateKeyCounter(){
        keyCounter = keyCounter + 1;
        keysCounterText.text = keyCounter.ToString() + "/" + totalKeys.ToString();
    }
}
