using UnityEngine;

public class EnemyMeleeRangeControl : MeleeRangeControl
{
    [SerializeField] SlimeEnemy slimeEnemy;
    protected override void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            // Health playerHealth = other.GetComponent<Health>();
            // if(playerHealth != null){
            //     playerHealth.TakeDamage(1);
            // }
            slimeEnemy.CheckDamagePlayer();
            DisableMeleeRange();
        }
    }
}
