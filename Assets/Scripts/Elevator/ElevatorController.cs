using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    // [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;

    private bool movingLeft;
    private bool movingUp;
    private bool hitThePlayer;
    private bool startMoving;

    void Start(){
        startMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startMoving){
            if(startPoint.position.x == endPoint.position.x && startPoint.position.y != endPoint.position.y){
                VerticalMove();
            }
            else if(startPoint.position.x != endPoint.position.x && startPoint.position.y == endPoint.position.y){
                HorizontalMove();
            }
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

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            startMoving = true;
        }
    }


}
