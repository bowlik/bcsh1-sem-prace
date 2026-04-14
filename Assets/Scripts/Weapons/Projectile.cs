using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Nastavení")]
    public int damage = 30;
    public float explosionRadius = 1.5f;
    public bool hasTimer = false;
    public float timerDuration = 3f;

    private float _timer;
    private bool _exploded = false;

    private void Start()
    {
        _timer = timerDuration;
    }

    private void Update()
    {
        if (hasTimer)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f) Explode();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (_exploded) return;
        if (!hasTimer) Explode();
    }

    private void Explode()
    {
        if (_exploded) return;
        _exploded = true;

        // znièí terén
        TerrainManager.Instance?.DestroyTerrain(transform.position, explosionRadius);

        // poškodí myši v polomìru
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<MouseController>(out var mouse))
            {
                // damage klesá s vzdáleností
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                float falloff = 1f - Mathf.Clamp01(dist / explosionRadius);
                int finalDamage = Mathf.RoundToInt(damage * falloff);
                mouse.TakeDamage(finalDamage);
            }
        }

        // TODO: pøidat particle efekt výbuchu
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}