using UnityEngine;

public class MeleeRangeControl : MonoBehaviour
{
    void Start(){
        DisableMeleeRange();
    }
    [SerializeField] BoxCollider2D meleeRangeBox;
    public void EnableMeleeRange(){
        meleeRangeBox.enabled = true;
    }

    public void DisableMeleeRange(){
        meleeRangeBox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            Health enemyHealth = other.GetComponent<Health>();
            if(enemyHealth != null){
                enemyHealth.TakeDamage(1);
            }
            DisableMeleeRange();
        }
    }
}
