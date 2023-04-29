using UnityEngine;
using System.Collections;

public class RockHeadTrap : Trap
{
    [Header("Rock Head Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    // [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    // [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform destination;

    [Header("Collider")]
    [SerializeField] private BoxCollider2D headCollider;
    [SerializeField] private Rigidbody2D body;

    [Header("Direction")]
    [SerializeField] private bool observeTheRight;
    [SerializeField] private bool observeTheLeft;
    [SerializeField] private bool observeAbove;
    [SerializeField] private bool observeBelow;
    private Vector3[] directions = new Vector3[4];


    [Header("Distance")]
    private float stoppingDistance;


    // private Vector3 destination;
    // private bool attacking;
    // private float checkTimer;
    [Header("Flags")]
    private bool attackFinished;
    private bool canAttack;
    // private bool isMoving;
    private bool noChangedYet;
    private bool hitThePlayer;

    [Header("Sound effects")]
    [SerializeField] private AudioClip impactSound;

    // Start is called before the first frame update
    void Start()
    {
        // damage = 1.0f;
        // range = 6.0f;
        // checkDelay = 1.0f;
        // speed = 2.0f;
        // CheckForPlayer();
        attackFinished = false;
        canAttack = false;
        // isMoving = false;
        stoppingDistance = 0.1f;
        noChangedYet = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the spikeHead to the destination only if attacking
        // if(attacking){
        //     bool playerIsHurting = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().isHurting;
        //     if(!playerIsHurting){
        //         transform.Translate(destination*Time.deltaTime*speed);
        //     }
        // }
        // else{
        //     checkTimer = checkTimer + Time.deltaTime;
        //     if(checkTimer > checkDelay){
        //         CheckForPlayer();
        //     }
        // }
        if(canAttack){
            Debug.Log("Player is in range");
            if(!attackFinished){
                Attack();
            }
            else{
                if(noChangedYet && !hitThePlayer){
                    ChangeToGround();
                    noChangedYet = false;
                }
                // if(attackFinished){
                // }
            }
        }
        else{
            CheckForPlayer();
        }
    }
    void ChangeToGround(){
        gameObject.layer = LayerMask.NameToLayer("Ground");
        headCollider.isTrigger = false;
        body.mass = 1.0f;
        body.gravityScale = 8.0f;
    }
    // IEnumerator MoveToDestinationCoroutine()
    // {
    //     // Set the isMoving flag to true to indicate that the object is currently moving
    //     isMoving = true;

    //     // Calculate the distance between the current position and the destination
    //     float distance = Vector3.Distance(transform.position, destination.position);

    //     // Keep moving the object towards the destination until it reaches the destination
    //     while (distance > 0.1f)
    //     {
    //         // Move the object towards the destination
    //         transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);

    //         // Update the distance between the current position and the destination
    //         distance = Vector3.Distance(transform.position, destination.position);

    //         // Wait for the next frame
    //         yield return null;
    //     }

    //     // Set the isMoving flag to false to indicate that the object has reached the destination and stopped moving
    //     isMoving = false;
    // }

    private void Attack(){
        // if(transform.position.x != destination.position.x && transform.position.y == destination.position.y){
        //     HorizontalAttack();
        // }
        // else if(transform.position.y != destination.position.y && transform.position.x == destination.position.x){
        //     VerticalAttack();
        // }
        float distance = Vector3.Distance(transform.position, destination.position);

        if(distance > stoppingDistance){
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
        }
        else{
            attackFinished = true;
        }

        // if (!isMoving)
        // {
        //     // Start the movement coroutine
        //     StartCoroutine(MoveToDestinationCoroutine());
        // }
        // attackFinished = true;
    }

    // private void HorizontalAttack(){
    //     if(transform.position.x != destination.position.x && transform.position.y == destination.position.y)
    //     {
    //         if(destination.position.x < transform.position.x){
    //             transform.position = new Vector3(transform.position.x - speed*Time.deltaTime, transform.position.y, transform.position.z);
    //         }
    //         else if(destination.position.x > transform.position.x){
    //             transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);
    //         }
    //         else{
    //             attackFinished = true;
    //         }
    //     }
    // }

    // private void VerticalAttack(){
    //     if(destination.position.y < transform.position.y){
    //         transform.position = new Vector3(transform.position.x, transform.position.y - speed*Time.deltaTime, transform.position.z);
    //     }
    //     else if(destination.position.y > transform.position.y){
    //         transform.position = new Vector3(transform.position.x, transform.position.y + speed*Time.deltaTime, transform.position.z);
    //     }
    //     else{
    //         attackFinished = true;
    //     }
    // }

    // private void OnEnable() {
    //     Stop();
    // }

    private void CheckForPlayer(){
        CalculateDirection();
        // Check if the spikeHead sees the player
        for(int i = 0; i < directions.Length; i++){
            if(directions[i] != null){
                Debug.DrawRay(transform.position, directions[i], Color.red);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

                // if(hit.collider != null && !attacking){
                //     attacking = true;
                //     destination = directions[i];
                //     checkTimer = 0;
                // }
                if(hit.collider != null){
                    canAttack = true;
                    return;
                }
            }
        }

        canAttack = false;
    }
    private void CalculateDirection(){
        if(observeTheRight){
            directions[0] = transform.right*range; // right direction
        }
        // else{
        //     directions[0] = Vector3.zero;
        // }
        if(observeTheLeft){
            directions[1] = -transform.right*range; // left direction
        }
        // else{
        //     directions[1] = Vector3.zero;
        // }
        if(observeAbove){
            directions[2] = transform.up*range; // up direction
        }
        // else{
        //     directions[2] = Vector3.zero;
        // }
        if(observeBelow){
            directions[3] = -transform.up*range; // down direction
        }
        // else{
        //     directions[3] = Vector3.zero;
        // }
    }
    // private void Stop(){
    //     destination = transform.position; // set the destination as current position, so it doesn't move
    //     attacking = false;
    // }
    protected override void OnTriggerEnter2D(Collider2D other) {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(other);
        if(other.tag == "Player"){
            hitThePlayer = true;
        }
        // Deactivate the trigger effect to the player can stand on
        // headCollider.isTrigger = false;
        // gameObject.layer = groundLayer;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            hitThePlayer = false;
        }
    }
}
