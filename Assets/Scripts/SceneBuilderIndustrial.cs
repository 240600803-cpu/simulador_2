using UnityEngine;

// Procedurally build a simple tunnel-like industrial scene using primitives.
// When you run the scene, attach this script to an empty GameObject and press Play.
public class SceneBuilderIndustrial : MonoBehaviour
{
    [Header("Tunnel settings")]
    public int lengthSegments = 10;
    public float segmentLength = 6f;
    public float tunnelRadius = 6f;
    public Material wallMaterial;

    [Header("Spawner refs")]
    public GameObject playerPrefab; // a simple cube with PlayerMovement
    public GameObject launcherPrefab; // a launcher prefab containing LauncherController
    public Transform spawnRoot;

    void Start()
    {
        BuildTunnel();
        PlacePlayerAndLauncher();
    }

    public void BuildTunnel()
    {
        if (spawnRoot == null)
        {
            spawnRoot = new GameObject("SceneRoot").transform;
            spawnRoot.SetParent(transform);
        }

        for (int i = 0; i < lengthSegments; i++)
        {
            float z = i * segmentLength;

            // Floor
            GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            floor.transform.localScale = new Vector3(tunnelRadius * 2f, 0.5f, segmentLength);
            floor.transform.position = new Vector3(0f, -0.25f, z);
            floor.transform.SetParent(spawnRoot);

            // Left wall
            GameObject left = GameObject.CreatePrimitive(PrimitiveType.Cube);
            left.transform.localScale = new Vector3(1f, tunnelRadius * 2f, segmentLength);
            left.transform.position = new Vector3(-tunnelRadius, tunnelRadius - 0.25f, z);
            left.transform.SetParent(spawnRoot);

            // Right wall
            GameObject right = GameObject.CreatePrimitive(PrimitiveType.Cube);
            right.transform.localScale = new Vector3(1f, tunnelRadius * 2f, segmentLength);
            right.transform.position = new Vector3(tunnelRadius, tunnelRadius - 0.25f, z);
            right.transform.SetParent(spawnRoot);

            // Ceiling
            GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
            ceiling.transform.localScale = new Vector3(tunnelRadius * 2f, 0.5f, segmentLength);
            ceiling.transform.position = new Vector3(0f, tunnelRadius * 2f - 0.25f, z);
            ceiling.transform.SetParent(spawnRoot);

            // Optional: add a light
            if (i % 3 == 0)
            {
                GameObject lightGO = new GameObject("PointLight");
                Light l = lightGO.AddComponent<Light>();
                l.type = LightType.Point;
                l.range = 15f;
                l.intensity = 1.2f;
                lightGO.transform.position = new Vector3(0f, tunnelRadius, z);
                lightGO.transform.SetParent(spawnRoot);
            }

            // Simple variation in wall color
            if (wallMaterial != null)
            {
                var mesh = floor.GetComponent<Renderer>();
                if (mesh != null) mesh.material = wallMaterial;
                var m2 = left.GetComponent<Renderer>(); if (m2 != null) m2.material = wallMaterial;
                var m3 = right.GetComponent<Renderer>(); if (m3 != null) m3.material = wallMaterial;
                var m4 = ceiling.GetComponent<Renderer>(); if (m4 != null) m4.material = wallMaterial;
            }
        }
    }

    public void PlacePlayerAndLauncher()
    {
        Vector3 pos = new Vector3(0f, 0.5f, 0f);
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, pos, Quaternion.identity, spawnRoot);
        }
        else
        {
            GameObject player = GameObject.CreatePrimitive(PrimitiveType.Cube);
            player.name = "Player";
            player.transform.position = pos;
            player.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            player.AddComponent<PlayerMovement>();
            player.transform.SetParent(spawnRoot);
        }

        Vector3 launcherPos = new Vector3(1.5f, 0.6f, 0f);
        if (launcherPrefab != null)
        {
            Instantiate(launcherPrefab, launcherPos, Quaternion.identity, spawnRoot);
        }
        else
        {
            GameObject launcher = new GameObject("Launcher");
            launcher.transform.position = launcherPos;
            var lc = launcher.AddComponent<LauncherController>();
            GameObject spawn = new GameObject("SpawnPoint");
            spawn.transform.SetParent(launcher.transform);
            spawn.transform.localPosition = new Vector3(0f, 0.5f, 1f);
            lc.spawnPoint = spawn.transform;
            // add a simple visual
            GameObject body = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            body.transform.SetParent(launcher.transform);
            body.transform.localPosition = Vector3.zero;
            body.transform.localScale = new Vector3(0.4f, 0.2f, 0.4f);
        }
    }
}
