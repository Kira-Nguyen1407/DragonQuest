using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] protected RectTransform[] options;
    [SerializeField] protected AudioClip optionChangeSound; // Play when we move option arrow up/down
    [SerializeField] protected AudioClip selectedSound; // Play when we select an option

    protected RectTransform rect;
    protected int currentPosition;
    // Start is called before the first frame update
    public virtual void Start()
    {
        rect = GetComponent<RectTransform>();
        currentPosition = 0;
        // Set the initial position of the arrow at "restart" option
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        KeyListener();
    }

    public virtual void ChangePosition(int change){
        currentPosition = currentPosition + change;

        if(currentPosition < 0){
            currentPosition = options.Length - 1;
        }
        else if(currentPosition > options.Length-1){
            currentPosition = 0;
        }
        // Move the option arrow up and down
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
        SoundManager.instance.PlaySound(optionChangeSound);
    }

    public virtual void KeyListener(){
        // Move the option arrow up or down
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            ChangePosition(-1);
        }
        else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            ChangePosition(1);
        }

        // Interact with the options 
        if(Input.GetKeyDown(KeyCode.Return)){
            InteractWithOptions();
        }
    }

    public virtual void InteractWithOptions(){
        // Access the button component on each option and call it's function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
