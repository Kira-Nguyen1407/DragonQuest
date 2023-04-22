using UnityEngine;

public class MeleeRangeControl : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D meleeRangeBox;

    public virtual void Start(){
        DisableMeleeRange();
    }
    public virtual void EnableMeleeRange(){
        meleeRangeBox.enabled = true;
    }

    public virtual void DisableMeleeRange(){
        meleeRangeBox.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            Health enemyHealth = other.GetComponent<Health>();
            if(enemyHealth != null){
                enemyHealth.TakeDamage(1);
            }
            DisableMeleeRange();
        }
    }
}
