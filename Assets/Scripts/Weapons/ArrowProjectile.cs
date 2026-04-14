using UnityEngine;

public class ArrowProjectile : Projectile
{
    private void Awake()
    {
        hasTimer = false;
        damage = 25;
        explosionRadius = 0.5f; // šíp má malý polomìr zásahu
    }

    private void Update()
    {
        // šíp se otáèí ve smìru letu
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null && rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}