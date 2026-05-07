using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Nastavení")]
    public int damage = 30;
    public float explosionRadius = 1.5f;
    public bool hasTimer = false;
    public float timerDuration = 3f;

    [Header("Efekty")]
    public GameObject explosionEffectPrefab;

    private float _timer;
    private bool _exploded = false;
    private GameObject _shooter; // myš která vystøelila

    private void Start()
    {
        _timer = timerDuration;
    }

    // zavolej tuto metodu pøi vytvoøení projektilu
    public void SetShooter(GameObject shooter)
    {
        _shooter = shooter;
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

        // ignoruj pouze myš která vystøelila
        // soupeøova myš zpùsobí výbuch!
        if (col.gameObject == _shooter) return;

        if (!hasTimer) Explode();
    }

    private void Explode()
    {
        if (_exploded) return;
        _exploded = true;

        if (explosionEffectPrefab != null)
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

        AudioManager.Instance?.PlayExplosion();

        if (TerrainManager.Instance != null)
            TerrainManager.Instance.DestroyTerrain(transform.position, explosionRadius);
        else
            Debug.LogError("TerrainManager.Instance je null!");

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position, explosionRadius);

        foreach (var hit in hits)
        {
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}