using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float turnChance = 0.01f;
    public float gameOverDelay = 0.5f;

    private SpriteRenderer sr;
    private float dir = 1f;
    private bool isDead;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        transform.position += Vector3.right * dir * speed * Time.deltaTime;

        if (Random.value < turnChance) dir = -dir;

        if (sr != null) sr.flipX = (dir < 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;
        if (!collision.collider.CompareTag("Player")) return;

        // Si el Player NO lo ha matado antes con el hitbox de pies, aquí muere el Player
        Destroy(collision.collider.gameObject);
        StartCoroutine(LoadGameOver());
    }

    private IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("GameOver");
    }
}
