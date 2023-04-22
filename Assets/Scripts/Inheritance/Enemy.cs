using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] protected float attackCoolDown;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;

    [Header("Collider parameters")]
    [SerializeField] protected BoxCollider2D boxCollider;
    [SerializeField] protected float colliderDistance;

    [Header("Player parameters")]
    [SerializeField] protected LayerMask playerLayer;
    [SerializeField] public Health playerHealth;
    [SerializeField] public Transform playerTransform;
    protected float coolDownTimer;

    [Header("Enemy parameters")]
    [SerializeField] protected Animator animator;

    // // Start is called before the first frame update
    protected virtual void Start()
    {
        coolDownTimer = Mathf.Infinity;
        // range = 5.0f;
        // colliderDistance = 0.0f;
        damage = 1.0f;
        attackCoolDown = 1.0f;
        animator = GetComponent<Animator>();
        // enemyPatrol = GetComponentInParent<EnemyPatrol>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    public virtual bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance,
            new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
                Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    public virtual void DamagePlayer(){
        if(PlayerInSight()){
            if(!playerHealth.isHurting && !playerHealth.dead){
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public virtual void OnDrawGizmos() {
        Gizmos.color = Color.red;
        // Gizmos.DrawCube(boxCollider.bounds.center, boxCollider.bounds.size);
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right*range*transform.localScale.x*colliderDistance,
            new Vector3(boxCollider.bounds.size.x*range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // // Update is called once per frame
    // protected virtual void Update()
    // {
        
    // }
}
