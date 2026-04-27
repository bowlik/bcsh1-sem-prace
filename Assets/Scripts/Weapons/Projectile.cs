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

        // ignoruj kolizi s myší která vystøelila
        if (col.gameObject.CompareTag("Mouse")) return;

        if (!hasTimer) Explode();
    }

    private void Explode()
    {
        if (_exploded) return;
        _exploded = true;

        Debug.Log($"Explode() na pozici {transform.position}, radius: {explosionRadius}");

        // particle efekt
        if (explosionEffectPrefab != null)
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

        // zvuk
        AudioManager.Instance?.PlayExplosion();

        // znièí terén
        if (TerrainManager.Instance != null)
            TerrainManager.Instance.DestroyTerrain(transform.position, explosionRadius);
        else
            Debug.LogError("TerrainManager.Instance je null!");

        // poškodí myši v polomìru
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position, explosionRadius);

        Debug.Log($"Nalezeno {hits.Length} colliderù v polomìru {explosionRadius}");

        foreach (var hit in hits)
        {
            Debug.Log($"Zasažen: {hit.gameObject.name}");

            if (hit.TryGetComponent<MouseController>(out var mouse))
            {
                float dist = Vector2.Distance(
                    transform.position, hit.transform.position);
                float falloff = 1f - Mathf.Clamp01(dist / explosionRadius);
                int finalDamage = Mathf.RoundToInt(damage * falloff);

                Debug.Log($"Poškození {mouse.gameObject.name}: {finalDamage}");
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