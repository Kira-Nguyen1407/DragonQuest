using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : Trap
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    // Start is called before the first frame update
    void Start()
    {
        damage = 1.0f;
        movementDistance = 3.0f;
        speed = 5.0f;
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        // playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movingLeft){
            if(transform.position.x > leftEdge){
                transform.position = new Vector3(transform.position.x - speed*Time.deltaTime, transform.position.y, transform.position.z);
            }
            else{
                movingLeft = false;
            }
        }
        else{
            if(transform.position.x < rightEdge){
                transform.position = new Vector3(transform.position.x + speed*Time.deltaTime, transform.position.y, transform.position.z);
            }
            else{
                movingLeft = true;
            }
        }
    }
}
