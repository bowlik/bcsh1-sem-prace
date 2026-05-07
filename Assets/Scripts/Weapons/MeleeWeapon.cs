using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    [Header("Melee")]
    public int damage = 90;
    public float knockbackForce = 12f;
    public float hitRange = 1.5f;

    protected override void Fire()
    {
        _hasFired = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            Owner.transform.position, hitRange);

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<MouseController>(out var mouse)) continue;
            if (mouse.gameObject == Owner.gameObject) continue;

            mouse.TakeDamage(damage);

            Rigidbody2D rb = mouse.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 dir = (mouse.transform.position - Owner.transform.position).normalized;
                rb.AddForce(dir * knockbackForce, ForceMode2D.Impulse);
            }
        }

        // zniš tile pøímo pod myší
        Vector2 pos = Owner.transform.position;
        TerrainManager.Instance?.DestroyTerrain(
            new Vector2(pos.x, pos.y - 0.5f), 2f);

        TurnManager.Instance?.EndTurn();
    }

    private void OnDrawGizmosSelected()
    {
        if (Owner == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Owner.transform.position, hitRange);
    }
}