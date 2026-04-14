using UnityEngine;

public class FireballWeapon : WeaponBase
{
    [Header("Ohnivá koule")]
    public float arcForce = 5f;

    protected override void Fire()
    {
        if (projectilePrefab == null) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 direction = (mouseWorld - transform.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // pøidáme mírný oblouk nahoru
            Vector2 force = direction * shootForce + Vector2.up * arcForce;
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        TurnManager.Instance?.EndTurn();
    }
}