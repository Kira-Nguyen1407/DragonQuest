using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth;
    [SerializeField] public float totalHealth;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;

    [Header("Player components")]
    [SerializeField] private SpriteRenderer spriteRend;
    [SerializeField] private CharacterAttack characterAttack;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private PlayerRespawn playerRespawn;
    private int playerLayer;

    [Header("Enemy components")]
    // private int enemiesLayer;
    [SerializeField] SlimeEnemy slimeEnemy;

    [Header("Shared components")]
    private Animator animator;
    public bool dead;
    public bool isHurting;
    // private Behaviour[] components;

    [Header("Sound components")]
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private AudioClip hurtSound;

    [Header("Game Over Screen")]
    [SerializeField] private GameOverScreenUI gameOverScreen;


    // private float flashesRate;

    // Start is called before the first frame update
    void Start()
    {
        // startingHealth = 5.0f;
        // currentHealth = startingHealth;
        // totalHealth = 10.0f;
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        // components = gameObject.GetComponents<Behaviour>();
        dead = false;
        playerLayer = 10;
        // enemiesLayer = 11;
        // flashesRate = 1.0f;
        iFramesDuration = 3.0f;
        numberOfFlashes = 3.0f;
    }

    public void TakeDamage(float _damage){
        if(currentHealth > 0){
            // the player is hurt
            if(!isHurting){
                currentHealth = Mathf.Clamp(currentHealth - _damage, 0, totalHealth);
                animator.SetTrigger("hurt");
                SoundManager.instance.PlaySound(hurtSound);
                if(gameObject.layer == playerLayer || slimeEnemy.isBig){
                    StartCoroutine(Invulnerability());
                }
            }
        }
        if(currentHealth <= 0){
            if(!dead){
                // // Set the grounded to true to prevent executing the jump animation instead of death animation
                // animator.SetBool("grounded", true);

                // Executing the death animation
                // animator.SetBool("grounded", false);
                animator.SetTrigger("die");
                SoundManager.instance.PlaySound(deadSound);
                dead = true;
            }
        }
    //     else{
            
    //         // the player dead
    //     }
    }

    public void Death(){
        if(currentHealth <= 0){
            animator.SetTrigger("die");
        }
    }

    public void EnableGameOverScreen(){
        gameOverScreen.ActivateGameOverScreen();
    }

    public void AddHealth(float healthPoint){
        currentHealth = Mathf.Clamp(currentHealth + healthPoint, 0, totalHealth);
    }

    public void ResetPlayer(){
        // Debug.Log("Resetting player");
        gameObject.SetActive(true);
        // ActivateRendered();
        ActivatePlayer();
        dead = false;
        AddHealth(startingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");
        StartCoroutine(Invulnerability());
    }

    public void ActivatePlayer(){
        // foreach (Behaviour component in components)
        // {
        //     component.enabled = true;
        // }
        spriteRend.enabled = true;
        characterAttack.enabled = true;
        characterMovement.enabled = true;
    }

    public void DeactivatePlayer(){
        // // Loop through all components and disable them, except for Animator
        // foreach (Behaviour component in components)
        // {
        //     // if (component.GetType() != typeof(Animator) && component.GetType() != typeof(Rigidbody2D))
        //     // {
        //     //     component.enabled = false;
        //     // }
        //     if (component.GetType() == typeof(SpriteRenderer))
        //     {
        //         component.enabled = false;
        //     }
        // }
        spriteRend.enabled = false;
        characterAttack.enabled = false;
        characterMovement.enabled = false;
    }

    public void Deactivate(){
        gameObject.SetActive(false);
    }

    // public void DeactivateRendered(){
    //     spriteRend.enabled = false;
    // }

    // public void ActivateRendered(){
    //     spriteRend.enabled = true;
    // }

    public void Activate(){
        gameObject.SetActive(true);
    }

    private IEnumerator Invulnerability(){
        // Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, true);
        isHurting = true;
        //invulnerability duration
        for(int i=0; i< numberOfFlashes * 2; i++){
            spriteRend.color = new Color(1,0,0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/ (numberOfFlashes*2));

            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/ (numberOfFlashes*2));
        }
        // Physics2D.IgnoreLayerCollision(playerLayer, enemiesLayer, false);
        isHurting = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.E)){
        //     // Debug.Log("Damage received");
        //     Debug.Log($"Current health: {currentHealth}");
        //     TakeDamage(1);
        // }
        StartCoroutine(CheckPlayerHealth());
    }

    IEnumerator CheckPlayerHealth(){
        if(currentHealth <= 0){
            animator.Play("Die");
        }

        yield return new WaitForSeconds(2);
    }
}
