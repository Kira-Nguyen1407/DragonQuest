using UnityEngine;

public class Collector : MonoBehaviour
{
    [Header("Health")]
    private float healthValue;

    [Header("Score")]
    [SerializeField] private ScoreManager scoreManager;

    [Header("Sound effects")]
    [SerializeField] private AudioClip collectedSound;

    [Header("Flags")]
    [SerializeField] private bool blueBook;

    // Start is called before the first frame update
    void Start()
    {
        healthValue = 1.0f;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            SoundManager.instance.PlaySound(collectedSound);
            if(gameObject.tag == "Heart"){
                other.GetComponent<Health>().AddHealth(healthValue);
            }
            else if(gameObject.tag == "Apple"){
                scoreManager.UpdateScore(10);
            }
            else if(gameObject.tag == "Cherry"){
                scoreManager.UpdateScore(20);
            }
            else if(gameObject.tag == "Raspberry"){
                scoreManager.UpdateScore(50);
            }
            if(gameObject.tag == "SecretBook"){
                if(blueBook){
                    other.GetComponent<CharacterMovement>().AddSecretBook();
                }
                else{
                    other.GetComponent<CharacterAttack>().AddRedSecretBook();
                }
                scoreManager.UpdateSecretBookCounter();

                
            }
            if(gameObject.tag == "Key"){
                scoreManager.UpdateKeyCounter();
            }
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
