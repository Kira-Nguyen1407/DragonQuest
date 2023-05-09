using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D body;
    [SerializeField] private Health playerHealth;
    [SerializeField] private CharacterAttack characterAttack;
    [SerializeField] private float initGravity;
    [SerializeField] private float initPlayerMass;
    private bool isFallingDown;
    private float checkingFallingDownPeriod;
    private float fallingSpeed;
    private float fallingTime;
    private float fallDownSpeed;
    private float maxSpeedAllowed;
    // public bool collidingWithObstacle;

    [Header("Collectables")]
    private int nSecretBooks;
    private int totalSecretBooks;
    public bool keyCollected;

    [Header("Animations")]
    public Animator animator;
    // public bool grounded;

    [Header("Colliders and Layers")]
    public BoxCollider2D boxCollider;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask wallLayer;
    [SerializeField] protected float xRange;
    [SerializeField] protected float yRange;

    [SerializeField] protected float colliderDistance;


    

    [Header("Normal Jump components")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpPower;
    private float jumpDistanceThreshold;
    public float horizontalInput;

    [Header("Sound components")]
    [SerializeField] private AudioClip jumpSound;

    // [Header("Coyote Time")]
    // [SerializeField] private float coyoteTime; // How much time the player can hang in the air before jumping
    // private float coyoteCounter; // How much time passed since the player ran off the edge

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps; // one for double jump, two for triple jump
    private int jumpCounter; // How many extra jumps we have at the moment

    [Header("Wall Jumping")]
    public bool isWallJumping;
    [SerializeField] public int totalWallJumps;
    private int wallJumpCounter;
    // [SerializeField] private float wallJumpX; // Horizontal wall jump forces
    // [SerializeField] private float wallJumpY; // Vertical wall jump forces


    // Start is called before the first frame update
    void Start()
    {
        // this.body = GetComponent<Rigidbody2D>();
        // this.moveSpeed = 5.0f;
        boxCollider = GameObject.Find("Character").GetComponent<BoxCollider2D>();
        jumpDistanceThreshold = 0.2f;
        totalWallJumps = 3;
        keyCollected = false;
        nSecretBooks = 0;
        totalSecretBooks = 3;
        checkingFallingDownPeriod = 0.05f;
        fallingTime = 0;
        isFallingDown = false;
        fallDownSpeed = 0.5f;
        maxSpeedAllowed = 1.5f;
        // collidingWithObstacle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerHealth.dead){
            // ShowPlayerVelocity();
            Move();
        }
        // if(wallJumpCoolDown < 0.2f){

        //     body.velocity = new Vector2(horizontalInput*moveSpeed, body.velocity.y);

        //     if(onWall() && !isGrounded()){
        //         body.gravityScale = 0;
        //         body.velocity = Vector2.zero;
        //     }
        //     else{
        //         body.gravityScale = 1;
        //     }

        //     if(Input.GetKey(KeyCode.W)){
        //         Jump();
        //     }
        // }
        // else{
        //     wallJumpCoolDown = wallJumpCoolDown + Time.deltaTime;
        // }
    }

    private void FixedUpdate() {
        isGrounded();
        if(isFallingDown){
            if(Time.frameCount % 5 == 0){
                fallingTime = fallingTime + 0.1f;
                fallingSpeed = body.gravityScale*fallingTime;
                // Debug.Log("Falling speed: " + fallingSpeed);
                if(fallingSpeed >= maxSpeedAllowed){
                    // Decrease the gravity and player's mass to prevent the player from falling through the ground
                    body.gravityScale = Mathf.Clamp(body.gravityScale - 3.5f, 0, initGravity);
                    body.mass = Mathf.Clamp(body.mass - 0.8f, 0, initPlayerMass);
                    // body.gravityScale = 0;
                    // body.mass = 0;
                }
            }
        }
    }

    IEnumerator CheckIfFallingDown(){
        float preYCoordinate = transform.position.y;
        yield return new WaitForSeconds(checkingFallingDownPeriod);
        float currentYCoordinate = transform.position.y;
        if(currentYCoordinate < preYCoordinate){
            isFallingDown = true;
        }
        else{
            isFallingDown = false;
            fallingTime = 0;
            // Reset the gravity and player's mass when the player no longer falls down
            body.gravityScale = initGravity;
            body.mass = initPlayerMass;
        }
    }

    // private void DecreasePlayerMass(){
    //     if(isFallingDown){
    //         if(fallingSpeed > maxSpeedAllowed){
    //             body.gravityScale = body.gravityScale - 0.5f;
    //             body.mass = body.mass - 0.2f;
    //         }
    //     }
    // }

    private void MoveOnTheGround(){
        SetDirection();
        animator.SetBool("run", horizontalInput != 0);
        body.velocity = new Vector2(horizontalInput*moveSpeed, body.velocity.y);

        // if(!collidingWithObstacle){
        //     Debug.Log("Moving on the ground");
        //     body.velocity = new Vector2(horizontalInput*moveSpeed, body.velocity.y);
        // }
    }
    private void HorizontalMoveInTheAir(){
        // Debug.Log("HorizontalMoveInTheAir");
        SetDirection();
        // body.velocity = new Vector2(horizontalInput*moveSpeed, body.velocity.y);

        if(CollidingWithObstacles()){
            Debug.Log("Colliding with an obstacle");

            if(horizontalInput != 0 && Mathf.Sign(horizontalInput) != Mathf.Sign(transform.localRotation.x)){
                if(!Input.GetKeyDown(KeyCode.W)){
                    body.velocity = new Vector2(0, body.velocity.y - fallDownSpeed);
                }
            }
        }
        else{
            Debug.Log("No longer colliding with an obstacle");
            body.velocity = new Vector2(horizontalInput*moveSpeed, body.velocity.y);
        }
        // if(!collidingWithObstacle){
        //     Debug.Log("Moving horizontal in the air");
        //     body.velocity = new Vector2(horizontalInput*moveSpeed, body.velocity.y);
        // }
        // else{
        //     // Debug.Log("The player is colliding with the obstacles");
        //     // collidingWithObstacle = false;
        // }
        // Debug.Log("horizontalInput: " + horizontalInput);

        // if(!CollideWithObstacles()){
        //     Debug.Log("The player is not colliding with the obstacles");
            
        // }
        // else{
        //     Debug.Log("Collide With Obstacles");
        // }
    }

    private void MultipleJump(){
        if(jumpCounter > 0){
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            SoundManager.instance.PlaySound(jumpSound);
            jumpCounter--;
        }
    }

    private void JumpFromTheGround(){
        animator.SetBool("grounded", false);

        body.velocity = new Vector2(body.velocity.x, jumpPower*1.5f);
        SoundManager.instance.PlaySound(jumpSound);
    }

    private void SetDirection(){
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip the character when moving left or right
        if(horizontalInput > 0.01f){
            transform.localScale = new Vector3(1.1f,1.1f,1);
        }
        else if(horizontalInput < -0.01f){
            transform.localScale = new Vector3(-1.1f,1.1f,1);
        }
    }

    public void AddSecretBook(){
        nSecretBooks++;
    }

    public bool CollectedAllSecretBooks(){
        if(nSecretBooks >= totalSecretBooks){
            return true;
        }

        return false;
    }

    public void Move(){
        StartCoroutine(CheckIfFallingDown());
        // if(!collidingWithObstacle){
        //     // Debug.Log("The player is not colliding with obstacles");
        // }
        // else{
        //     Debug.Log("The player is colliding with the obstacles");
        // }
        if(isGrounded()){
            // Reset falling time if the player fell down before
            // fallingTime = 0;
            // Reset falling flag because the player is grounding now
            // isFallingDown = false;

            wallJumpCounter = 0;
            // Reset the gravity scale after performing wall jump
            body.gravityScale = 5.0f;
            // Reset the isWallJumping to move the main camera as normal
            isWallJumping = false;
            // Reset the jumpCounter when the player on the ground
            jumpCounter = extraJumps;
            // Stop the jump animation
            animator.SetBool("grounded", true);

            if(!onWall()){
                
                if(Input.GetKeyDown(KeyCode.W)){
                    JumpFromTheGround();
                }
                else{
                    // Debug.Log("Grounded");
                    MoveOnTheGround();
                }
            }
            else{
                // Debug.Log("On the wall!");
                // Debug.Log("Horizontal Input: " + horizontalInput);
            
                horizontalInput = Input.GetAxis("Horizontal");

                // Normal jump when encountering the wall
                if(Input.GetKeyDown(KeyCode.W) && horizontalInput == 0){
                    animator.SetBool("grounded", false);
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    SoundManager.instance.PlaySound(jumpSound);
                }
                // Do wall jump if the player collected all secret books
                else if(Input.GetKeyDown(KeyCode.W) && horizontalInput != 0 && 
                        Mathf.Sign(horizontalInput) == Mathf.Sign(transform.localScale.x))
                {
                    if(CollectedAllSecretBooks()){
                        WallJump();
                    }
                }
                else{
                    // Debug.Log("Moving on the ground");
                    MoveOnTheGround();
                    // if(isGrounded()){
                        
                    // }
                    // else{
                    //     HorizontalMoveInTheAir();
                    // }
                }
            }
        }
        else{
            // Debug.Log("Here");
            animator.SetBool("grounded", false);
            horizontalInput = Input.GetAxis("Horizontal");

            if(onWall()){
                jumpCounter = extraJumps;
                // Debug.Log("horizontalInput: " + horizontalInput);
                // Debug.Log("localScale.x: " + transform.localScale.x);

                if(Mathf.Sign(horizontalInput) != Mathf.Sign(transform.localScale.x) && horizontalInput != 0){
                    // Debug.Log("Start moving in the air");
                    HorizontalMoveInTheAir();
                }
                else{
                    if(CollectedAllSecretBooks()){
                        WallJump();
                    }
                }
            }
            else{
                // Debug.Log("the player is not on the wall");
                body.gravityScale = 5.0f;
                if(CollectedAllSecretBooks()){
                    if(Input.GetKeyDown(KeyCode.W)){
                        MultipleJump();
                    }
                }
                
                HorizontalMoveInTheAir();
            }
        }

        // // Set animator parameters
        // animator.SetBool("grounded", isGrounded());

        // // Jump
        // if(Input.GetKeyDown(KeyCode.W)){
        //     Jump();
        // }

        // // Adjustable jump height
        // if(Input.GetKeyUp(KeyCode.W) && body.velocity.y > 0){
        //     body.velocity = new Vector2(body.velocity.x, body.velocity.y/2.0f);
        // }

        // if(onWall()){
        //     body.gravityScale = 1;
        //     // body.velocity = Vector2.zero;
        // }
        // else{
        //     body.gravityScale = 3;
        //     body.velocity = new Vector2(horizontalInput*moveSpeed, body.velocity.y);

        //     if(isGrounded()){
        //         isWallJumping = false;
        //         coyoteCounter = coyoteTime; // Reset the coyote counter when the player is on the ground
        //     }
        //     else{
        //         coyoteCounter = coyoteCounter - Time.deltaTime; // Decrease the coyote counter when the player is not on the ground
        //     }
        // }
    }

    private void WallJump(){
        isWallJumping = true;
        body.gravityScale = 2.0f;
        // Debug.Log("wallJumpCounter: " + wallJumpCounter);
        if(horizontalInput != 0 && Input.GetKeyDown(KeyCode.W)){
            if(wallJumpCounter < totalWallJumps){
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*moveSpeed, jumpPower*0.6f);
                wallJumpCounter++;
            }
            // body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)*horizontalInput*moveSpeed, wallJumpY));
            // body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)*wallJumpX, wallJumpY));
        }
        
        // if(horizontalInput == 0){
        //     body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*10, 0);
        //     transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        // }
        // else{
        //     body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3, 5);

        // }
        //       
        // wallJumpCoolDown = 0; 
    }

    public bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 7.0f, groundLayer);
        if(raycastHit.collider != null){
            // Debug.Log("Distance from the ground: " + raycastHit.distance);
            if(raycastHit.distance > jumpDistanceThreshold){
                return false;
            }
            else{
                // if(raycastHit.collider.gameObject.transform.position.y > transform.position.y){
                //     Debug.Log("Here");
                //     transform.position = new Vector3(transform.position.x, raycastHit.collider.gameObject.transform.position.y + 1.0f, transform.position.z);
                // }
                return true;
            }
        }
        
        return false;
        // return raycastHit.collider != null;
    }

    // private void OnCollisionEnter2D(Collision2D collision) {
    //     if(collision.gameObject.tag == "Ground"){
    //         // grounded = true;
    //     }
    // }

    public bool onWall(){
        // // set up the raycast parameters
        // Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left; // check direction player is facing
        // Vector2 position = transform.position;
        // float distance = 0.7f;

        // // perform the raycast
        // RaycastHit2D raycastHit = Physics2D.Raycast(position, direction, distance, wallLayer);
        // // RaycastHit2D raycastHit = Physics2D.Raycast(position, direction, distance);

        // // RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), distance, wallLayer);
        // // if(raycastHit.collider != null){
        // //     Debug.Log("Distance from the ground: " + raycastHit.distance);
        // //     if(raycastHit.distance > jumpDistanceThreshold){
        // //         return false;
        // //     }
        // // }
        
        // // return true;
        // return raycastHit.collider != null;
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*xRange*transform.localScale.x*colliderDistance,
            new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y*yRange, boxCollider.bounds.size.z), 0,
                new Vector2(transform.localScale.x,0), 0, wallLayer);
        // RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance,
        //     new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y*range, boxCollider.bounds.size.z), 0,
        //         Vector2.left, 0, wallLayer);

        return hit.collider != null;
    }

    public bool CollidingWithObstacles(){

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*xRange*transform.localScale.x*colliderDistance,
            new Vector3(boxCollider.bounds.size.x*xRange, boxCollider.bounds.size.y*yRange, boxCollider.bounds.size.z), 0,
                new Vector2(transform.localScale.x,0), 0, groundLayer);

        if(hit.collider != null){
            if(hit.collider.gameObject.tag != "CheckingDoor"){
                return true;
            }
        }

        return false;
    }

    public virtual void OnDrawGizmos() {
        Gizmos.color = Color.red;
        // Gizmos.DrawCube(boxCollider.bounds.center, boxCollider.bounds.size);
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right*xRange*transform.localScale.x*colliderDistance,
            new Vector3(boxCollider.bounds.size.x*xRange, boxCollider.bounds.size.y*yRange, boxCollider.bounds.size.z));
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     collidingWithObstacle = true;
    // }

    // private void OnCollisionExit2D(Collision2D other) {
    //     collidingWithObstacle = false;
    // }

    // public bool CollideWithObstacles(){
    //     float distance = 0.1f; // adjust this value to your preference
    //     RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), distance);
        
    //     return raycastHit.collider != null && !onWall();

    // }

    public bool canAttack(){
        return !onWall();
        // return horizontalInput == 0 && isGrounded() && !onWall();
    }

    public void ShowPlayerVelocity(){
        if(!isGrounded()){
            float freeFallVelocity = body.velocity.y;
            Debug.Log("Free fall velocity of the player: " + freeFallVelocity);
            Debug.Log("positions: " + body.position.y);
        }
    }
}

    // public void Jump(){
    //     /* If coyote is 0 or less and the player is not on the wall and no more extra jumps,
    //         then we don't do anything
    //     */
    //     if(coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;

    //     SoundManager.instance.PlaySound(jumpSound);

    //     // // get the current animation state
    //     // AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    //     // // check if the current animation is playing
    //     // if (stateInfo.IsName("Jump") && characterAttack.isAttacking)
    //     // {
    //     //     // stop the current animation
    //     //     animator.speed = 0f;

    //     //     // play another animation instead
    //     //     // animator.Play("Die");
    //     // }

    //     if(onWall()){
    //         WallJump();
    //     }
    //     else{
    //         isWallJumping = false;
    //         if(isGrounded()){
    //             body.velocity = new Vector2(body.velocity.x, jumpPower);
    //             jumpCounter = extraJumps; // Reset the jumpCounter to the extraJumps
    //         }
    //         else{
    //             // If the player is not on the ground and coyote counter bigger than 0, do a normal jump
    //             if(coyoteCounter > 0){
    //                 body.velocity = new Vector2(body.velocity.x, jumpPower);
    //             }
    //             else{
    //                 // If we have extra jumps, then jump and decrease the jumpCounter variable
    //                 if(jumpCounter > 0){
    //                     body.velocity = new Vector2(body.velocity.x, jumpPower);
    //                     jumpCounter--;
    //                 }
    //             }

    //             // Reset the coyote counter to avoid the double jump
    //             coyoteCounter = 0;
    //         }
    //     }

    //     // if(isGrounded()){
    //     //     body.velocity = new Vector2(body.velocity.x, jumpPower);
    //     //     // animator.SetTrigger("jump");
    //     // }
    //     // else if(onWall() && !isGrounded()){
    //     //     if(horizontalInput == 0){
    //     //         body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*10, 0);
    //     //         transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    //     //     }
    //     //     else{
    //     //         body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x)*3, 5);

    //     //     }
    //     //     wallJumpCoolDown = 0;
    //     // }
        
    //     // isGrounded() = false;
    // }
