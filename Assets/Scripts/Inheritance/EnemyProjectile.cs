using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Trap
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip fireballExplode;
    private float lifeTime;
    private bool hit;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5.0f;
        resetTime = 5.0f;
        lifeTime = 0.0f;
        damage = 1.0f;
        animator = GetComponent<Animator>();
    }

    public void SetDirection(float _direction){

    }

    public void ActivateProjectile(){
        lifeTime = 0;
        hit = false;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(hit) return;
        float movementSpeed = speed*Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime = lifeTime + Time.deltaTime;
        if(lifeTime > resetTime){
            gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        hit = true;
        if(animator != null){
            // Debug.Log("animator is not null");
            animator.SetTrigger("explode");
            SoundManager.instance.PlaySound(fireballExplode);
        }
        else{
            gameObject.SetActive(false);
        }
    }

    void Deactivate() {
        gameObject.SetActive(false);
    }
}
