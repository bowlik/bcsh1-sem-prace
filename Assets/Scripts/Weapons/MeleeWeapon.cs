using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    [Header("Melee")]
    public int damage = 35;
    public float knockbackForce = 12f;
    public float hitRange = 1.5f;

    protected override void Fire()
    {
        // zasáhne všechny myši v dosahu
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            Owner.transform.position, hitRange);

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<MouseController>(out var mouse)) continue;
            if (mouse == Owner) continue; // nezasáhne sám sebe

            mouse.TakeDamage(damage);

            // knockback
            Rigidbody2D rb = mouse.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 dir = (mouse.transform.position - Owner.transform.position).normalized;
                rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
            }
        }

        TurnManager.Instance?.EndTurn();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hitRange);
    }
}