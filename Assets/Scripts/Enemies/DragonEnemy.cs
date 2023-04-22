using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemy : Enemy
{
    // Update is called once per frame
    [Header("Dragon Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    [Header("Fireball Sound")]
    [SerializeField] private AudioClip _sound;

    // [SerializeField] private SpriteRenderer dragonEnemyRenderer;

    protected override void Start()
    {
        base.Start();
        // dragonEnemyRenderer.color = new Color(120, 134, 171);
    }

    void Update()
    {
        if(PlayerInSight()){
            if(coolDownTimer >= attackCoolDown){
                // Attack
                coolDownTimer = 0;
                animator.SetTrigger("rangeAttack");
            }
            else{
                coolDownTimer = coolDownTimer + Time.deltaTime;
            }
        }
    }

    private void RangedAttack(){
        SoundManager.instance.PlaySound(_sound);
        coolDownTimer = 0;
        // Shoot projectiles
        int fireBallIndex = findFireBalls();
        fireballs[fireBallIndex].transform.position = firePoint.position;
        fireballs[fireBallIndex].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int findFireBalls(){
        for(int i=0; i<fireballs.Length; i++){
            if(!fireballs[i].activeInHierarchy){
                return i;
            }
        }

        return 0;
    }
}
