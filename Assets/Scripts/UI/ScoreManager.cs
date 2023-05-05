using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text keysCounterText;
    [SerializeField] private Text SBsCounterText; // Secret books
    private int secretBookCounter;
    private int keyCounter;
    private int totalKeys;
    private int totalSecretBooks;

    private void Start(){
        secretBookCounter = 0;
        keyCounter = 0;
        totalKeys = 1;
        totalSecretBooks = 3;
        UpdateScore(0);
    }

    public void UpdateScore(int _score){
        if(scoreText != null){
            PersistenceData.totalScore = PersistenceData.totalScore + _score;
            scoreText.text = PersistenceData.totalScore.ToString();
        }
        
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
