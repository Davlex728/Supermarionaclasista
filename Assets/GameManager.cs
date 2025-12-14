using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public Transform SpawnPoint;

    public int amount = 10;
    public float interval = 1f;

    public float maxX = 10f;
    public float minX = -10f;
    float timer;
    int spawned;

    void Start()
    {
        timer = interval;
        spawned = 0;
    }

    void Update()
    {
        if (spawned >= amount) return;

        timer -= Time.deltaTime;
        if (timer > 0f) return;

        Vector3 spawnPosition = SpawnPoint != null ? SpawnPoint.position : transform.position;
        Quaternion spawnRotation = SpawnPoint != null ? SpawnPoint.rotation : transform.rotation;

        Instantiate(EnemyPrefab, spawnPosition, spawnRotation);
        spawned++;
        timer = interval;

        GameObject[] entities = GameObject.FindGameObjectsWithTag("Entity");
        for (int i = 0; i < entities.Length; i++)
        {
            Vector3 p = entities[i].transform.position;
            if (p.x > maxX) p.x = minX;
            else if (p.x < minX) p.x = maxX;
            entities[i].transform.position = p;
        }
    }
}
