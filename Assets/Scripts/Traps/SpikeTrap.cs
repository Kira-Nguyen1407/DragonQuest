using UnityEngine;

public class SpikeTrap : Trap
{
    void Start()
    {
        damage = 1.0f;
    }

    /*
        This trap only uses the OnTriggerStay2D method of the base class, this empty OnTriggerEnter2D method is 
        created to prevent the player from being able to get damaged doubly
    */
    protected override void OnTriggerEnter2D(Collider2D other){}
}
