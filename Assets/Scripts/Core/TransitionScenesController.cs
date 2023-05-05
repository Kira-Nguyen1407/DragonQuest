using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TransitionScenesController : MonoBehaviour
{
    [SerializeField] private Image previousSceneImg;
    [SerializeField] private Image nextSceneImg;
    [SerializeField] private GameObject checkingDoor;
    private float changingPeriod;
    private float alphaValue;
    // private bool isFadingIn = false;
    // private bool isFadingOut = false;
    private bool startFading = false;
    // Start is called before the first frame update
    void Start()
    {
        // alphaValue = 0;
        changingPeriod = 0.1f;
    }

    private IEnumerator FadeIn(){
        // isFadingIn = true;
        startFading = true;
        alphaValue = 0;
        while(alphaValue <= 1){
            Color currentColor = previousSceneImg.color;
            currentColor.a = alphaValue;
            previousSceneImg.color = currentColor;
            alphaValue = alphaValue + 0.1f;
            yield return new WaitForSeconds(changingPeriod);
        }
        // isFadingIn = false;
    }

    private IEnumerator FadeOut(){
        startFading = true;
        alphaValue = 1.0f;
        // isFadingOut = true;
        while(alphaValue >= 0){
            Color currentColor = nextSceneImg.color;
            currentColor.a = alphaValue;
            nextSceneImg.color = currentColor;
            alphaValue = alphaValue - 0.1f;
            yield return new WaitForSeconds(changingPeriod);
        }
        // isFadingOut = false;
    }

    private void CheckForFading(){
        if(checkingDoor != null){
            if(!checkingDoor.activeInHierarchy){
                if(previousSceneImg != null){
                    if(!startFading){
                        StartCoroutine(FadeIn());
                    }
                }
            }
        }

        if(nextSceneImg != null){
            if(!startFading){
                StartCoroutine(FadeOut());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForFading();
    }
}
