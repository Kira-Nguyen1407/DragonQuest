using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    [SerializeField] private AudioClip rangedAttackSound;
    [SerializeField] private AudioClip meleeAttackSound;
    [SerializeField] private MeleeRangeControl meleeRangeControl;
    
    public bool isAttacking;


    // public GameObject fireballPrefab;

    private Animator animator;
    private CharacterMovement characterMovement;
    private float coolDownTimer;
    private bool meleeAttack;

    [Header("Collectables")]
    private int nSecretBooks;
    private int totalSecretBooks;

    [Header("Flags")]
    [SerializeField] private bool inLevel1;

    // public Projectile projectile;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterMovement = GetComponent<CharacterMovement>();
        coolDownTimer = Mathf.Infinity;
        // attackCoolDown = 0.2f;
        meleeAttack = true;
        totalSecretBooks = 3;
        if(inLevel1){
            nSecretBooks = 0;
        }
        else{
            nSecretBooks = totalSecretBooks;
        }
        // projectile = GameObject.FindGameObjectWithTag("FireBall").GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Space) && coolDownTimer > attackCoolDown && characterMovement.canAttack()){
            Attack();
        }
        else{
            isAttacking = false;
        }

        coolDownTimer = coolDownTimer + Time.deltaTime;
    }

    public void AddRedSecretBook(){
        // nSecretBooks = PlayerPrefs.GetInt("nRedSecretBooks");
        nSecretBooks++;
        // PlayerPrefs.SetInt("nRedSecretBooks", nSecretBooks);
        // PlayerPrefs.Save();
    }

    // void ResetNumberOfSecretBooks(){
    //     PlayerPrefs.SetInt("nRedSecretBooks", 0);
    // }

    // void CheckNumOfSecretBooks(){
    //     nSecretBooks = PlayerPrefs.GetInt("nRedSecretBooks");
    //     PlayerPrefs.SetInt("nRedSecretBooks", nSecretBooks);
    // }

    public bool CollectedAllRedSecretBooks(){
        if(nSecretBooks >= totalSecretBooks){
            return true;
        }

        return false;
    }

    private void Attack(){
        isAttacking = true;
        if(meleeAttack){
            // CheckNumOfSecretBooks();
            // Perform melee attack
            if(CollectedAllRedSecretBooks()){
                SoundManager.instance.PlaySound(meleeAttackSound);
                animator.SetTrigger("meleeAttack");
            }
        }
        else{
            // Perform ranged attack
            SoundManager.instance.PlaySound(rangedAttackSound);
            animator.SetTrigger("rangeAttack");
            // coolDownTimer = 0;

            int fireBallIndex = findFireBalls();
            fireBalls[fireBallIndex].transform.position = firePoint.position;
            float localScaleX = transform.localScale.x;
            fireBalls[fireBallIndex].GetComponent<Projectile>().SetDirection(Mathf.Sign(localScaleX));
        }
        coolDownTimer = 0;

    }

    public void EnableMeleeRangeCollider(){
        meleeRangeControl.EnableMeleeRange();
    }

    public void DisableMeleeRangeCollider(){
        meleeRangeControl.DisableMeleeRange();
    }

    private int findFireBalls(){
        for(int i=0; i<fireBalls.Length; i++){
            if(!fireBalls[i].activeInHierarchy){
                return i;
            }
        }

        return 0;
    }
}
