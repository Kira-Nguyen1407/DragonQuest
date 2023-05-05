using UnityEngine;

public class SawTrap : Trap
{
    // [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private bool movingLeft;
    private bool movingUp;
    
    // private float leftEdge;
    // private float rightEdge;

    // Start is called before the first frame update
    void Start()
    {
        // damage = 1.0f;
        // movementDistance = 3.0f;
        // speed = 5.0f;
        // leftEdge = transform.position.x - movementDistance;
        // rightEdge = transform.position.x + movementDistance;
        // playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startPoint.position.x == endPoint.position.x && startPoint.position.y != endPoint.position.y){
            VerticalMove();
        }
        else if(startPoint.position.x != endPoint.position.x && startPoint.position.y == endPoint.position.y){
            HorizontalMove();
        }
    }

    private void HorizontalMove(){
        if(movingLeft){
            if(transform.position.x > startPoint.position.x){
                transform.position = new Vector3(transform.position.x - speed*Time.deltaTime, transform.position.y, transform.position.z);
            }
            else{
                movingLeft = false;
            }
        }
        else{
            if(transform.position.x < endPoint.position.x){
                transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);
            }
            else{
                movingLeft = true;
            }
        }
    }

    private void VerticalMove(){
        if(movingUp){
            if(transform.position.y < endPoint.position.y){
                // Debug.Log("Moving up");
                transform.position = new Vector3(transform.position.x, transform.position.y + speed*Time.deltaTime, transform.position.z);
            }
            else{
                movingUp = false;
            }
        }
        else{
            if(transform.position.y > startPoint.position.y){
                // Debug.Log("Moving down");
                transform.position = new Vector3(transform.position.x, transform.position.y - speed*Time.deltaTime, transform.position.z);
            }
            else{
                movingUp = true;
            }
        }
    }

    /*
        This trap only uses the OnTriggerStay2D method of the base class, this empty OnTriggerEnter2D method is 
        created to prevent the player from being able to get damaged doubly
    */
    protected override void OnTriggerEnter2D(Collider2D other){}
}
