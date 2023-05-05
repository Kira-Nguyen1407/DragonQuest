using UnityEngine;
using UnityEngine.SceneManagement;


public class CheckingDoor : MonoBehaviour
{
    // [Header("Checking types")]
    // [SerializeField] private bool checkSecretBooks;
    // [SerializeField] private bool checkKey;
    // [SerializeField] private bool checkEnemiesDefeated;

    [Header("Player Components")]
    [SerializeField] private CharacterMovement characterMovement;

    [Header("Enemies components")]
    // [SerializeField] private Health[] enemies;
    [SerializeField] private GameObject[] checkingObjects;

    [Header("Flags")]
    private bool disableFlag;
    [SerializeField] private bool isFinalDoor;
    [SerializeField] private bool inLevel1;

    [Header("Dialog Trigger")]
    [SerializeField] private DialogTrigger dialogTrigger;


    // [Header("Checking variables")]
    // private bool collectedAllSecretBooks;
    // private bool collectedAllKeys;
    // private bool defeatedAllEnemies;

    private void Start() {
        // collectedAllKeys = false;
        // collectedAllSecretBooks = false;
        // defeatedAllEnemies = false;
        disableFlag = false;
    }

    // function to check if the player collected all secret books, SBs stands for secret books
    // private void CheckSBsCollected(){
    //     if(characterMovement.CollectedAllSecretBooks()){
    //         collectedAllSecretBooks = true;
    //     }
    // }

    // private void CheckEnemiesStatus(){
    //     int i = 0;
    //     for(; i < enemies.Length; i++){
    //         // if(enemies[i].dead == false){
    //         //     break;
    //         // }
    //         if(enemies[i].activeInHierarchy){
    //             break;
    //         }
    //     }
    //     if(i == enemies.Length-1){
    //         defeatedAllEnemies = true;
    //     }
    // }

    // private void CheckCollectionOfKeys(){
    //     if(characterMovement.keyCollected == true){
    //         collectedAllKeys = true;
    //     }
    // }

    private void DeactivateDoor(){
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            if(disableFlag){
                if(isFinalDoor){
                    if(inLevel1){
                        SceneManager.LoadScene("Level2");
                    }
                    else{
                        SceneManager.LoadScene("End");
                    }
                }
                DeactivateDoor();
            }
            else{
                dialogTrigger.StartDialog();
            }
            
        }
    }

    private void Check(){
        int i = 0;
        for(; i < checkingObjects.Length; i++){
            if(checkingObjects[i].activeInHierarchy){
                break;
            }
        }
        if(i == checkingObjects.Length){
            disableFlag = true;
        }
    }

    private void Update() {
        if(!disableFlag){
            Check();
        }
        // if(checkKey){
        //     if(!collectedAllKeys){
        //         CheckCollectionOfKeys();
        //     }
        // }
        // if(checkSecretBooks){
        //     if(!collectedAllSecretBooks){
        //         CheckSBsCollected();
        //     }
        // }
        // if(checkEnemiesDefeated){
        //     if(!defeatedAllEnemies){
        //         CheckEnemiesStatus();
        //     }
        // }
    }
}
