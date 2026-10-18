using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 8f;
    public GameObject hitEffect;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision other)
    {
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 0.05f);
    }
}
