using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room camera
    [SerializeField] private float speed;
    private float currentPositionX;
    private Vector3 velocity;

    // Flow the cha
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Transform character;
    [SerializeField] private CharacterMovement playerMovement;
    private float lookAhead;
    // private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        // speed = 3.0f;
        aheadDistance = 2.0f;
        cameraSpeed = 3.0f;
        // lookAhead = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPositionX, 
        // //     transform.position.y, transform.position.z), ref velocity, speed * Time.deltaTime);

        // // Camera only flows the character when it is grounded
        // if(!playerMovement.isWallJumping){
        //     transform.position = new Vector3(character.position.x + lookAhead, transform.position.y, transform.position.z);
        //     lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * character.localScale.x), Time.deltaTime * cameraSpeed);
        // }
        CameraMovement();
    }

    private void CameraMovement(){
        if(!playerMovement.isWallJumping){
            transform.position = new Vector3(character.position.x + lookAhead, character.position.y, transform.position.z);
            // previousPosition = transform.position;;
            lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * character.localScale.x), Time.deltaTime * cameraSpeed);
        }
        // else{
        //     transform.position = previousPosition;
        // }
    }
}
