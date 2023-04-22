using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float coolDownTimer;

    // [Header("Sound components")]
    // [SerializeField] private AudioClip flyingArrowSound;

    void Start()
    {
        coolDownTimer = 0.0f;
        attackCoolDown = 1.0f;
    }

    public void Attack(){

        coolDownTimer = 0;

        arrows[findArrow()].transform.position = firePoint.position;
        // SoundManager.instance.PlaySound(flyingArrowSound);
        arrows[findArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    public int findArrow(){
        for(int i = 0; i < arrows.Length; i++){
            if(!arrows[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        coolDownTimer = coolDownTimer + Time.deltaTime;

        if(coolDownTimer > attackCoolDown){
            Attack();
        }
    }
}
