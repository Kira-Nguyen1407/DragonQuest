using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemy : Enemy
{
    [Header("Enemy parameters")]
    [SerializeField] private KnightEnemyPatrol knightEnemyPatrol;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip swordHitSound;


    // Update is called once per frame
    void Update()
    {
        if(PlayerInAttackRange()){
            if(coolDownTimer >= attackCoolDown){
                // Attack
                coolDownTimer = 0;
                animator.SetTrigger("meleeAttack");
                SoundManager.instance.PlaySound(swordHitSound);


            }
            else{
                coolDownTimer = coolDownTimer + Time.deltaTime;
            }
        }
        
        // if(enemyPatrol != null){
        //     enemyPatrol.enabled = !PlayerInSight();
        // }
    }

    public override bool PlayerInSight()
    {
        if(playerTransform.position.x >= knightEnemyPatrol.leftEdge.position.x && 
            playerTransform.position.x <= knightEnemyPatrol.rightEdge.position.x)
        {
            return true;
        }

        return false;
    }

    public bool PlayerInAttackRange(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance,
            new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
                Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    public override void DamagePlayer()
    {
        if(PlayerInAttackRange()){
            if(!playerHealth.isHurting && !playerHealth.dead){
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
