using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float turnChance = 0.01f;

    SpriteRenderer sr;
    float dir = 1f;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.position += Vector3.right * dir * speed * Time.deltaTime;

        if (Random.value < turnChance) dir = -dir;

        if (sr != null) sr.flipX = (dir < 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}