using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private AudioClip endingMusicClip;
    private float waitingTime = 2.0f;

    public void Quit(){
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator PlayEndingMusic(){
        yield return new WaitForSeconds(waitingTime);
        SoundManager.instance.PlaySound(endingMusicClip);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayEndingMusic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
