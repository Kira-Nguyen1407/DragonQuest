using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] AudioClip collectSound;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            other.GetComponent<CharacterMovement>().keyCollected = true;
            SoundManager.instance.PlaySound(collectSound);
            DeactivateKey();
        }
    }

    private void DeactivateKey(){
        gameObject.SetActive(false);
    }
}
