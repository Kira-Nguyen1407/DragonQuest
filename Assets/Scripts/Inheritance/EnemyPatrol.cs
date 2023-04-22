using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] public Transform leftEdge;
    [SerializeField] public Transform rightEdge;

    [Header("Enemy")]
    // [SerializeField] protected Transform enemy;
    [SerializeField] protected GameObject enemy;
    protected Vector3 enemyPosition;
    protected Transform enemyTransform;

    [Header("Movement Parameters")]
    [SerializeField] public float speed;
    // public float speed;

    protected Vector3 initScale;
    protected bool movingLeft;

    [Header("Enemy Animators")]
    [SerializeField] protected Animator animator;

    [Header("Idle Behavior")]
    [SerializeField] protected float idleDuration;
    protected float idleTimeCounter;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // initScale = enemy.localScale;
        // speed = initSpeed;
        enemyTransform = enemy.GetComponent<Transform>();
        initScale =  enemyTransform.localScale;
        enemyPosition =  enemyTransform.position;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     // if(movingLeft){
    //     //     if(enemy.position.x >= leftEdge.position.x){
    //     //         MoveInDirection(-1);
    //     //     }
    //     //     else{
    //     //         // Change direction
    //     //         DirectionChange();

    //     //     }
    //     // }
    //     // else{
    //     //     if(enemy.position.x <= rightEdge.position.x){
    //     //         MoveInDirection(1);
    //     //     }
    //     //     else{
    //     //         // Change direction
    //     //         DirectionChange();

    //     //     }
    //     // }
    // }

    protected virtual void DirectionChange(){
        animator.SetBool("moving", false);

        idleTimeCounter = idleTimeCounter + Time.deltaTime;
        if(idleTimeCounter >= idleDuration){
            movingLeft = !movingLeft;
        }

    }

    protected virtual void MoveInDirection(float _direction){
        idleTimeCounter = 0;
        animator.SetBool("moving", true);

        // Make the enemy facing the direction
        // enemy.localScale = new Vector3(Mathf.Abs(initScale.x)*_direction, enemy.localScale.y, enemy.localScale.z);

        // Move the enemy to the direction
        // enemy.position = new Vector3(enemy.position.x + Time.deltaTime*_direction*speed, enemy.position.y, enemy.position.z);
    }

    public virtual void DeactivatePatrol(){
        gameObject.SetActive(false);
    }

    // protected virtual void OnDisable() {
    //     animator.SetBool("moving", false);
    // }
}
