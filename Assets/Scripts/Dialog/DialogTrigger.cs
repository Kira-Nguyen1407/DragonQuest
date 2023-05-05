using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [Header("DiaLog Components")]
    [SerializeField] private DialogManager dialogManager;
    [SerializeField] private GameObject dialogBox;
    public Message[] messages;
    public Actor[] actors;
    private int countDisplayTime;
    private int maxDisplayTime;

    [Header("Screens")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;

    [Header("Flags")]
    [SerializeField] public bool isNotice;
    private bool startedDialog;

    [Header("Player Components")]
    [SerializeField] private CharacterAttack characterAttack;
    [SerializeField] private CharacterMovement characterMovement;


    private void Start() {
        countDisplayTime = 0;
        maxDisplayTime = 2;
    }


    public void StartDialog(){
        if(!isNotice){
            PauseThePlayer();
            dialogBox.SetActive(true);
            startedDialog = true;
            dialogManager.OpenDialog(messages, actors);
        }
        else{
            if(countDisplayTime < maxDisplayTime){
                dialogBox.SetActive(true);
                startedDialog = true;
                dialogManager.OpenDialog(messages, actors);
                countDisplayTime++;
            }
        }
        
    }

    public void PauseThePlayer(){
        Time.timeScale = 0;
        characterAttack.enabled = false;
        characterMovement.enabled = false;
    }

    public void UnPausedThePlayer(){
        Time.timeScale = 1;
        characterAttack.enabled = true;
        characterMovement.enabled = true;
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if(other.gameObject.CompareTag("Player")){
    //         StartDialog();
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(gameObject.tag == "InvisibleTrigger"){
                gameObject.SetActive(false);
            }
            StartDialog();
        }
    }

    private void Update() {
        if(isNotice){
            if(startedDialog){
                StartCoroutine(dialogManager.DisplayTheNotices());
            }
        }
        
        if(dialogManager.conversationEnded){
            startedDialog = false;

            dialogBox.SetActive(false);

            if(pauseScreen != null && gameOverScreen != null){
                // Debug.Log("Pause Screen is not null");
                if(!gameOverScreen.activeInHierarchy && !pauseScreen.activeInHierarchy){
                    UnPausedThePlayer();
                }
            }
            // else{
            //     Debug.Log("Pause Screen is null");
            // }

            // Do not put this line ahead of the line that checks if the game over screen and the pause screen are null

        }
    }
}

[System.Serializable]
public class Message{
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor{
    public string name;
    public Sprite sprite;
}