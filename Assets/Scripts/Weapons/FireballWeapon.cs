using UnityEngine;

public class FireballWeapon : WeaponBase
{
    [Header("Ohnivá koule")]
    public float arcForce = 5f;

    protected override void Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("FireballWeapon: Projectile Prefab není pøiøazený!");
            return;
        }

        _hasFired = true;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - Owner.transform.position).normalized;

        Vector3 spawnPos = Owner.transform.position + (Vector3)(direction * 1.2f);

        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        Projectile p = proj.GetComponent<Projectile>();
        if (p != null)
            p.SetShooter(Owner.gameObject);

        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 force = direction * shootForce + Vector2.up * arcForce;
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        TurnManager.Instance?.EndTurn();
    }
}