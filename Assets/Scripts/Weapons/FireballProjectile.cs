using UnityEngine;

public class FireballProjectile : Projectile
{
    [Header("Vizuál")]
    public TrailRenderer trail;

    private SpriteRenderer _sr;

    private void Awake()
    {
        hasTimer = false;
        damage = 40;
        explosionRadius = 2f;
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // debug – zkontroluj jestli je sprite renderer aktivní
        if (_sr != null)
        {
            Debug.Log($"Fireball visible: {_sr.enabled}, color: {_sr.color}, sprite: {_sr.sprite}");
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null && rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(
                rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}