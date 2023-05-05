using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    // [SerializeField] private DialogTrigger dialogTrigger;
    // [SerializeField] private GameObject dialogBox;
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActor;
    int activeMessage = 0;
    public bool conversationEnded;
    // public static bool isActive = false;

    public void OpenDialog(Message[] messages, Actor[] actors){
        gameObject.SetActive(true);
        currentMessages = messages;
        currentActor = actors;
        activeMessage = 0;
        conversationEnded = false;
        // isActive = true;

        Debug.Log("Started conversation! Loaded messages: " + currentMessages.Length);
        DisplayMessage();
    }

    public void DisplayMessage(){
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActor[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public IEnumerator DisplayTheNotices(){
        for(int i = 0; i< currentMessages.Length; i++){
            yield return new WaitForSeconds(2);
            NextMessage();
        }
    }

    public void NextMessage(){
        activeMessage++;
        if(activeMessage < currentMessages.Length){
            DisplayMessage();
        }
        else{
            Debug.Log("Conversation ended successfully!");
            conversationEnded = true;
            // isActive = false;
            // if(!dialogTrigger.isNotice){
            //     dialogTrigger.UnPausedThePlayer();
            // }
            // gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(isActive){
        //     if(dialogTrigger.isNotice){
        //         StartCoroutine(DisplayTheNotices());
        //     }
        // }
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     NextMessage();
        // }
    }
}
