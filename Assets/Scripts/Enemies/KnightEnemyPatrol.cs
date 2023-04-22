using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightEnemyPatrol : EnemyPatrol
{
    private KnightEnemy knightEnemy;
    private bool _playerInSight;
    private bool _playerInAttackRange;
    // Update is called once per frame

    public override void Start() {
        base.Start();
        knightEnemy = enemy.GetComponent<KnightEnemy>();
    }

    protected override void MoveInDirection(float _direction){
        idleTimeCounter = 0;
        animator.SetBool("moving", true);

        // Make the enemy facing the direction
        enemyTransform.localScale = new Vector3(Mathf.Abs(initScale.x)*_direction, enemyTransform.localScale.y, enemyTransform.localScale.z);

        // Move the enemy to the direction
        if(_playerInSight){
            _playerInAttackRange = knightEnemy.PlayerInAttackRange();

            if(_playerInAttackRange){
                animator.SetBool("moving", false);
            }
            else{
                enemyTransform.position = new Vector3(enemyTransform.position.x + Time.deltaTime*_direction*speed*3, enemyTransform.position.y, enemyTransform.position.z);
            }
        }
        else{
            enemyTransform.position = new Vector3(enemyTransform.position.x + Time.deltaTime*_direction*speed, enemyTransform.position.y, enemyTransform.position.z);
        }
    }

    void Update()
    {
        if(!knightEnemy.playerHealth.dead){
            _playerInSight = knightEnemy.PlayerInSight();
            enemyPosition = enemyTransform.position;
            if(_playerInSight){
                if(knightEnemy.playerTransform.position.x < enemyPosition.x){
                    if(enemyPosition.x > leftEdge.position.x){
                        MoveInDirection(-1);
                    }
                }
                if(knightEnemy.playerTransform.position.x > enemyPosition.x){
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
