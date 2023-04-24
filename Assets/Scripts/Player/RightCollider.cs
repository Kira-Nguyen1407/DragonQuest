using UnityEngine;

public class RightCollider : MonoBehaviour
{
    [SerializeField] CharacterMovement playerMovement;

    private void OnCollisionEnter2D(Collision2D other) {
        playerMovement.collidingWithObstacle = true;
    }

    private void OnCollisionExit2D(Collision2D other) {
        playerMovement.collidingWithObstacle = false;
    }
}
