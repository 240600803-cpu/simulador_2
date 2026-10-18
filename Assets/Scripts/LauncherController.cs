using UnityEngine;

public class LauncherController : MonoBehaviour
{
    [Header("Launch settings")]
    public Transform spawnPoint;
    public GameObject projectilePrefab;
    public float minForce = 5f;
    public float maxForce = 40f;
    public float chargeSpeed = 20f;

    [Header("Control")]
    public float rotationSpeed = 60f;

    private float currentForce;
    private bool charging;

    void Start()
    {
        currentForce = minForce;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.SetCharge(currentForce, minForce, maxForce);
        }
    }

    void Update()
    {
        float rot = 0f;
        if (Input.GetKey(KeyCode.Q)) rot = -1f;
        if (Input.GetKey(KeyCode.E)) rot = 1f;
        transform.Rotate(Vector3.up, rot * rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            charging = true;
            currentForce = minForce;
        }

        if (Input.GetKey(KeyCode.Space) && charging)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            currentForce = Mathf.Min(currentForce, maxForce);
            if (UIManager.Instance != null)
            {
                UIManager.Instance.SetCharge(currentForce, minForce, maxForce);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && charging)
        {
            Fire(currentForce);
            charging = false;
            currentForce = minForce;
            if (UIManager.Instance != null)
            {
                UIManager.Instance.SetCharge(0f, minForce, maxForce);
            }
        }
    }

    void Fire(float force)
    {
        if (projectilePrefab == null || spawnPoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(spawnPoint.forward * force, ForceMode.Impulse);
        }
    }
}
