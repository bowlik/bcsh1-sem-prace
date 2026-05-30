using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class MagicBombProjectile : MonoBehaviour
{
    [Header("Nastavení")]
    public int damage = 99;
    public float explosionRadius = 2.5f;
    public int maxBounces = 2;

    [Header("Efekty")]
    public GameObject explosionEffectPrefab;

    private int _bounceCount = 0;
    private bool _exploded = false;
    private GameObject _shooter;
    private Rigidbody2D _rb;
    private CameraFollow _camera;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Start()
    {
        _camera = FindFirstObjectByType<CameraFollow>();
        _camera?.TrackBomb(transform);
    }

    public void SetShooter(GameObject shooter)
    {
        _shooter = shooter;

        Collider2D projCollider = GetComponent<Collider2D>();
        Collider2D shooterCollider = shooter?.GetComponent<Collider2D>();
        if (projCollider != null && shooterCollider != null)
            Physics2D.IgnoreCollision(projCollider, shooterCollider);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_exploded) return;
        if (col.gameObject == _shooter) return;
        if (_shooter != null && col.transform.IsChildOf(_shooter.transform)) return;

        _bounceCount++;

        if (_bounceCount > maxBounces)
            Explode();
    }

    private void Explode()
    {
        if (_exploded) return;
        _exploded = true;

        // zastav sledování granátu
        _camera?.StopTrackingBomb();

        if (explosionEffectPrefab != null)
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

        AudioManager.Instance?.PlayExplosion();

        TerrainManager.Instance?.DestroyTerrain(transform.position, explosionRadius);

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position, explosionRadius);

        foreach (var hit in hits)
        {
            if (hit.gameObject == _shooter) continue;
            if (_shooter != null && hit.transform.IsChildOf(_shooter.transform)) continue;

            if (hit.TryGetComponent<MouseController>(out var mouse))
            {
                float dist = Vector2.Distance(
                    transform.position, hit.transform.position);
                float falloff = 1f - Mathf.Clamp01(dist / explosionRadius);
                int finalDamage = Mathf.RoundToInt(damage * falloff);
                mouse.TakeDamage(finalDamage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}