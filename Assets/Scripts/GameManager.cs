using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject EnemyPrefab;
    public Transform SpawnPoint;
    public int amount = 10;
    public float interval = 1f;

    [Header("Screen Wrap (Layer)")]
    public string wrapLayerName = "Wrap";
    public float maxX = 10f;
    public float minX = -10f;

    private float timer;
    private int spawned;
    private int wrapLayer;

    void Start()
    {
        timer = interval;
        spawned = 0;

        wrapLayer = LayerMask.NameToLayer(wrapLayerName);
        if (wrapLayer == -1)
            Debug.LogError($"Layer '{wrapLayerName}' no existe. Créala en Project Settings > Tags and Layers.");
    }

    void Update()
    {
        HandleSpawning();
        HandleWrap();
    }

    private void HandleSpawning()
    {
        if (spawned >= amount) return;

        timer -= Time.deltaTime;
        if (timer > 0f) return;

        Vector3 spawnPosition = SpawnPoint != null ? SpawnPoint.position : transform.position;
        Quaternion spawnRotation = SpawnPoint != null ? SpawnPoint.rotation : transform.rotation;

        Instantiate(EnemyPrefab, spawnPosition, spawnRotation);

        spawned++;
        timer = interval;
    }

    private void HandleWrap()
    {
        if (wrapLayer == -1) return;

        Collider2D[] all = FindObjectsByType<Collider2D>(FindObjectsSortMode.None);
        for (int i = 0; i < all.Length; i++)
        {
            GameObject go = all[i].gameObject;
            if (go.layer != wrapLayer) continue;

            Vector3 p = go.transform.position;
            if (p.x > maxX) p.x = minX;
            else if (p.x < minX) p.x = maxX;

            go.transform.position = p;
        }
    }
}
