using UnityEngine;

public class MainMenuSelection : SelectionArrow
{
    [SerializeField] private MainMenuSoundManager mainMenuSoundManager;

    public override void ChangePosition(int change){
        currentPosition = currentPosition + change;

        if(currentPosition < 0){
            currentPosition = options.Length - 1;
        }
        else if(currentPosition > options.Length-1){
            currentPosition = 0;
        }
        // Move the option arrow up and down
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
        // Play the moving arrow sound
        mainMenuSoundManager.PlaySound(optionChangeSound);
    }
}
