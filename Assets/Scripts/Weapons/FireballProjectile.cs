using UnityEngine;

public class FireballProjectile : Projectile
{
    [Header("Vizuál")]
    public TrailRenderer trail;

    private void Awake()
    {
        // ohnivá koule exploduje pøi dopadu, ne èasovaèem
        hasTimer = false;
        damage = 40;
        explosionRadius = 2f;
    }

    private void Update()
    {
        // otoèí sprite ve smìru letu
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null && rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}