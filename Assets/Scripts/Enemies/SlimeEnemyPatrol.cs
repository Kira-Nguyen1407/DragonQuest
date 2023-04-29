using UnityEngine;

public class SlimeEnemyPatrol : EnemyPatrol
{
    private SlimeEnemy slimeEnemy;
    private bool _playerInSight;
    private bool _playerInAttackRange;
    // Update is called once per frame

    public override void Start() {
        base.Start();
        slimeEnemy = enemy.GetComponent<SlimeEnemy>();
        movingLeft = true;
    }

    protected override void MoveInDirection(float _direction){
        idleTimeCounter = 0;
        animator.SetBool("moving", true);

        // Make the enemy facing the direction
        enemyTransform.localScale = new Vector3(-Mathf.Abs(initScale.x)*_direction, enemyTransform.localScale.y, enemyTransform.localScale.z);

        // Move the enemy to the direction
        if(_playerInSight){
            Debug.Log("player in sight");
            _playerInAttackRange = slimeEnemy.PlayerInAttackRange();

            if(_playerInAttackRange){
                animator.SetBool("moving", false);
            }
            else{
                // enemyTransform.position = new Vector3(enemyTransform.position.x + Time.deltaTime*_direction*speed*3, enemyTransform.position.y, enemyTransform.position.z);
                if(slimeEnemy.isBig){
                    enemyTransform.position = new Vector3(enemyTransform.position.x + Time.deltaTime*_direction*speed*3, enemyTransform.position.y, enemyTransform.position.z);
                }
                else{
                    enemyTransform.position = new Vector3(enemyTransform.position.x + Time.deltaTime*_direction*speed, enemyTransform.position.y, enemyTransform.position.z);
                }
            }
        }
        else{
            enemyTransform.position = new Vector3(enemyTransform.position.x + Time.deltaTime*_direction*speed, enemyTransform.position.y, enemyTransform.position.z);
        }
    }

    void Update()
    {
        if(!slimeEnemy.playerHealth.dead){
            _playerInSight = slimeEnemy.PlayerInSight();
            enemyPosition = enemyTransform.position;
            if(_playerInSight && slimeEnemy.isBig){
                if(slimeEnemy.playerTransform.position.x < enemyPosition.x){
                    if(enemyPosition.x > leftEdge.position.x){
                        MoveInDirection(-1);
                    }
                }
                if(slimeEnemy.playerTransform.position.x > enemyPosition.x){
                    if(enemyPosition.x < rightEdge.position.x){
                        MoveInDirection(1);
                    }
                }
            }
            else{
                if(movingLeft){
                    if(enemyPosition.x >= leftEdge.position.x){
                        MoveInDirection(-1);
                    }
                    else{
                        // Change direction
                        DirectionChange();
                    }
                }
                else{
                    if(enemyPosition.x <= rightEdge.position.x){
                        MoveInDirection(1);
                    }
                    else{
                        // Change direction
                        DirectionChange();
                    }
                }
            }
        }
        
        if(enemy.GetComponent<Health>().dead){
            DeactivatePatrol();
        }
    }
}
