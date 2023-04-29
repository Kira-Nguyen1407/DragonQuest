using UnityEngine;

public class SlimeEnemy : Enemy
{
    [Header("Enemy parameters")]
    [SerializeField] private SlimeEnemyPatrol slimeEnemyPatrol;
    [SerializeField] private Health enemyHealth;
    [SerializeField] private Range enemyRange;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip meleeHitSound;

    [Header("Attack Point Collider")]
    [SerializeField] private EnemyMeleeRangeControl attackPoint;


    // Update is called once per frame
    void Update()
    {
        if(!enemyHealth.dead){
            if(PlayerInAttackRange()){
                if(coolDownTimer >= attackCoolDown){
                    // Attack
                    coolDownTimer = 0;
                    animator.SetTrigger("meleeAttack");
                    SoundManager.instance.PlaySound(meleeHitSound);
                }
                else{
                    coolDownTimer = coolDownTimer + Time.deltaTime;
                }
            }
        }
        else{
            enemyHealth.Deactivate();
        }
        
        
        // if(enemyPatrol != null){
        //     enemyPatrol.enabled = !PlayerInSight();
        // }
    }

    public override bool PlayerInSight()
    {
        return enemyRange.playerInSight;
        // if(playerTransform.position.x >= slimeEnemyPatrol.leftEdge.position.x && 
        //     playerTransform.position.x <= slimeEnemyPatrol.rightEdge.position.x)
        // {
        //     if(playerTransform.position.y >= slimeEnemyPatrol.leftEdge.position.y - yRange/3 && 
        //         playerTransform.position.y >= slimeEnemyPatrol.rightEdge.position.y - yRange/3 &&
        //             playerTransform.position.y < slimeEnemyPatrol.leftEdge.position.y + yRange &&
        //                 playerTransform.position.y < slimeEnemyPatrol.rightEdge.position.y + yRange)
        //     {
        //         return true;
        //     }
        // }

        // return false;
    }

    public bool PlayerInAttackRange(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance,
            new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
                Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    public void CheckDamagePlayer(){
        if(PlayerInAttackRange()){
            DamagePlayer();
        }
    }

    // public override void DamagePlayer()
    // {
    //     if(PlayerInAttackRange()){
    //         if(!playerHealth.isHurting && !playerHealth.dead){
    //             playerHealth.TakeDamage(damage);
    //         }
    //     }
    // }

    public void DisableAttackPoint(){
        attackPoint.DisableMeleeRange();
    }

    public void EnabledAttackPoint(){
        attackPoint.EnableMeleeRange();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Health playerHealth = other.GetComponent<Health>();
            playerHealth.TakeDamage(1);
        }
    }
}
