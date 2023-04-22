using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    // [SerializeField] private Transform character;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player"){
            if(collision.transform.position.x < transform.position.x){
                if(nextRoom != null){
                    nextRoom.GetComponent<Room>().ActivateRoom(true);
                }
                if(previousRoom != null){
                    previousRoom.GetComponent<Room>().ActivateRoom(false);
                }
            }
            else{
                if(nextRoom != null){
                    nextRoom.GetComponent<Room>().ActivateRoom(false);
                }
                if(previousRoom != null){
                    previousRoom.GetComponent<Room>().ActivateRoom(true);
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // flow player
    }
}
