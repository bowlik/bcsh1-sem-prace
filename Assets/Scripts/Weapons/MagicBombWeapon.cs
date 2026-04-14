using UnityEngine;

public class MagicBombWeapon : WeaponBase
{
    [Header("Magická bomba")]
    public float throwForce = 8f;
    public float timerDuration = 3f;

    protected override void Fire()
    {
        if (projectilePrefab == null) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        GameObject bomb = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

        // nastav èasovaè na projektilu
        Projectile proj = bomb.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.hasTimer = true;
            proj.timerDuration = timerDuration;
            proj.damage = 60;
            proj.explosionRadius = 3f;
        }

        TurnManager.Instance?.EndTurn();
    }
}