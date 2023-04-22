using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    private int currentScore;

    private void Start(){
        currentScore = 0;
    }

    public void UpdateScore(int _score){
        currentScore = currentScore + _score;
        scoreText.text = currentScore.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
