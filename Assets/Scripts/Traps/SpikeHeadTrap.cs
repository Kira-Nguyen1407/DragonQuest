using UnityEngine;

public class SpikeHeadTrap : Trap
{
    [Header("Spike Head Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private bool attacking;
    private float checkTimer;


    [Header("Sound effects")]
    [SerializeField] private AudioClip impactSound;

    // Start is called before the first frame update
    void Start()
    {
        // damage = 1.0f;
        // range = 6.0f;
        // checkDelay = 1.0f;
        // speed = 2.0f;
        CheckForPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the spikeHead to the destination only if attacking
        if(attacking){
            bool playerIsHurting = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().isHurting;
            if(!playerIsHurting){
                transform.Translate(destination*Time.deltaTime*speed);
            }
        }
        else{
            checkTimer = checkTimer + Time.deltaTime;
            if(checkTimer > checkDelay){
                CheckForPlayer();
            }
        }
    }

    private void OnEnable() {
        Stop();
    }

    private void CheckForPlayer(){
        CalculateDirection();
        // Check if the spikeHead sees the player
        for(int i = 0; i < directions.Length; i++){
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if(hit.collider != null && !attacking){
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirection(){
        directions[0] = transform.right*range; // right direction
        directions[1] = -transform.right*range; // left direction
        directions[2] = transform.up*range; // up direction
        directions[3] = -transform.up*range; // down direction
    }
    private void Stop(){
        destination = transform.position; // set the destination as current position, so it doesn't move
        attacking = false;
    }
    protected override void OnTriggerEnter2D(Collider2D other) {
        SoundManager.instance.PlaySound(impactSound);
        base.OnTriggerEnter2D(other);
        // Stop the spikeHead once he his something
        Stop();
    }
}
