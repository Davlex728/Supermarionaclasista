using UnityEngine;

public class PlayerStomp : MonoBehaviour
{
    public float bounceForce = 6f;

    private Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        Destroy(other.gameObject);

        if (playerRb != null)
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, bounceForce);
    }
}
