using UnityEngine;

public class ArrowProjectile : Projectile
{
    private void Awake()
    {
        hasTimer = false;
        damage = 25;
        explosionRadius = 0f;
    }

    private void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null && rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(
                rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == _shooter) return;
        if (_shooter != null && col.transform.IsChildOf(_shooter.transform)) return;

        if (col.collider.TryGetComponent<MouseController>(out var mouse))
            mouse.TakeDamage(damage);

        Destroy(gameObject);
    }
}