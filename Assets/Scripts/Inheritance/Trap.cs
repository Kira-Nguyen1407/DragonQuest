using UnityEngine;

public class Trap : MonoBehaviour
{
    protected float damage;

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Health playerHealth = other.GetComponent<Health>();
            if(playerHealth.isHurting){
                damage = 0;
            }
            else{
                damage = 1;
            }
            playerHealth.TakeDamage(damage);
        }
    }
}
