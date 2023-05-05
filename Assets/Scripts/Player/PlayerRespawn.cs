using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip checkpointSound; // Play when the player pick up a new checkpoint
    private Transform currentCheckpoint; // Store the last checkpoint
    private Health playerHealth; 
    private Vector3 initPosition; // Store the initial position of the player
    [SerializeField]private GameOverScreenUI gameOverScreen;

    void Start()
    {
        playerHealth = GetComponent<Health>();
        initPosition = transform.position;
    }

    public void Respawn(){
        if(currentCheckpoint != null){
            transform.position = currentCheckpoint.position; // Move the player to the last checkpoint
        }
        else{
            transform.position = initPosition;
        }
        playerHealth.ResetPlayer(); // Reset the player's health and animation
    }

    // Activate the Checkpoint
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Checkpoint" || other.tag == "StartPoint"){
            currentCheckpoint = other.transform;
            SoundManager.instance.PlaySound(checkpointSound);
            other.GetComponent<Collider2D>().enabled = false; // Deactivate the checkpoint's collider
            other.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}
