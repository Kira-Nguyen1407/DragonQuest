using UnityEngine;

public class Range : MonoBehaviour
{
    public bool playerInSight;

    private void Start() {
        playerInSight = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            playerInSight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player"){
            playerInSight = false;
        }
    }
}
