using UnityEngine;

public class DragonEnemyPatrol : EnemyPatrol
{
    private DragonEnemy dragonEnemy;
    private bool _playerInSight;

    public override void Start() {
        base.Start();
        dragonEnemy = enemy.GetComponent<DragonEnemy>();
    }

    protected override void MoveInDirection(float _direction){
        idleTimeCounter = 0;
        animator.SetBool("moving", true);

        // Make the enemy facing the direction
        enemyTransform.localScale = new Vector3(Mathf.Abs(initScale.x)*_direction, enemyTransform.localScale.y, enemyTransform.localScale.z);

        // Move the enemy to the direction
        if(_playerInSight){
            animator.SetBool("moving", false);
        }
        else{
            enemyTransform.position = new Vector3(enemyTransform.position.x + Time.deltaTime*_direction*speed, enemyTransform.position.y, enemyTransform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!dragonEnemy.playerHealth.dead){
            _playerInSight = dragonEnemy.PlayerInSight();
            enemyPosition = enemyTransform.position;
            if(_playerInSight){
                if(dragonEnemy.playerTransform.position.x < enemyPosition.x){
                    if(enemyPosition.x > leftEdge.position.x){
                        MoveInDirection(-1);
                    }
                }
                if(dragonEnemy.playerTransform.position.x > enemyPosition.x){
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
