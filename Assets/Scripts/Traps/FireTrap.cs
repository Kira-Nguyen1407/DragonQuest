using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : Trap
{
    [Header("FireTrap Timers")] 
    [SerializeField] private float activationDelay; // how much time the trap needs to be activated after the player has stepped on it
    [SerializeField] private float activeTime;
    private Animator animator;
    private SpriteRenderer spRenderer;
    private bool triggered; // when the trap gets triggered
    private bool activated; // when the trap is activated and can hurt the player

    [Header("Sound effects")]
    [SerializeField] private AudioClip fireTrapSound;


    void Start(){
        animator = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();
        damage = 1.0f;
        triggered = false;
        activated = false;
        activationDelay = 2.0f;
        activeTime = 2.0f;
    }

    override protected void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            if(!triggered){
                // trigger the fire trap
                StartCoroutine(ActivateFireTrap());
            }
            
            if(activated){
                base.OnTriggerEnter2D(other);
            }
        }
    }

    IEnumerator ActivateFireTrap(){
        triggered = true;
        spRenderer.color = Color.red; // turn the sprite to red to notify the player

        // Wait for the delay, active trap, turn on the animation, return the color back to normal
        yield return new WaitForSeconds(activationDelay);
        spRenderer.color = Color.white; // turn the sprite back to its normal
        animator.SetBool("activated", true);
        activated = true;
        SoundManager.instance.PlaySound(fireTrapSound);

        // Wait for the delay to complete, deactivate the animation and trap
        yield return new WaitForSeconds(activeTime);
        activated = false;
        triggered = false;
        animator.SetBool("activated", false);
    }
}
