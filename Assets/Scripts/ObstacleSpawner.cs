using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawner Area")]
    public Vector3 center = Vector3.zero;
    public Vector3 size = new Vector3(20f, 2f, 40f);

    [Header("Obstacle")]
    public GameObject obstaclePrefab; // if null, a cube will be used
    public int obstacleCount = 12;
    public Vector2 scaleRange = new Vector2(0.5f, 2f);

    public bool spawnOnStart = true;
    private List<GameObject> spawned = new List<GameObject>();

    void Start()
    {
        if (spawnOnStart) SpawnAll();
    }

    public void SpawnAll()
    {
        Clear();
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 pos = center + new Vector3(
                Random.Range(-size.x / 2f, size.x / 2f),
                Random.Range(0.25f, size.y),
                Random.Range(-size.z / 2f, size.z / 2f)
            );

            GameObject obj;
            if (obstaclePrefab != null)
            {
                obj = Instantiate(obstaclePrefab, pos, Quaternion.identity, transform);
            }
            else
            {
                obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = pos;
                obj.transform.SetParent(transform);
                float s = Random.Range(scaleRange.x, scaleRange.y);
                obj.transform.localScale = new Vector3(s, s, s);
                var rb = obj.AddComponent<Rigidbody>();
                rb.mass = 5f;
            }

            spawned.Add(obj);
        }
    }

    public void Clear()
    {
        foreach (var g in spawned)
        {
            if (g != null) Destroy(g);
        }
        spawned.Clear();
    }
}
