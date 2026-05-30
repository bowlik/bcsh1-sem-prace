using UnityEngine;

public class MagicBombWeapon : WeaponBase
{
    [Header("Magická bomba")]
    public float throwForce = 8f;
    public LineRenderer aimLine;

    private bool _charging = false;

    private void OnEnable()
    {
        _charging = false;
        HideAimLine();
    }

    private void Update()
    {
        if (Owner == null || !Owner.IsActive) return;
        if (_hasFired) return;

        if (Input.GetMouseButtonDown(0))
            _charging = true;

        if (_charging && Input.GetMouseButton(0))
            DrawAimLine();

        if (_charging && Input.GetMouseButtonUp(0))
        {
            _charging = false;
            HideAimLine();
            Fire();
        }
    }

    protected override void Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("MagicBombWeapon: Projectile Prefab není pøiøazený!");
            return;
        }

        _hasFired = true;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - Owner.transform.position).normalized;
        Vector3 spawnPos = Owner.transform.position + (Vector3)(direction * 1.0f);

        GameObject bomb = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        bomb.GetComponent<MagicBombProjectile>()?.SetShooter(Owner.gameObject);

        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.AddForce(direction * throwForce, ForceMode2D.Impulse);

        TurnManager.Instance?.EndTurn();
    }

    private void DrawAimLine()
    {
        if (aimLine == null) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - Owner.transform.position).normalized;

        Vector2 spawnPos = (Vector2)Owner.transform.position + direction * 1.0f;

        aimLine.enabled = true;
        int points = 30;
        aimLine.positionCount = points;

        Vector2 pos = spawnPos;
        Vector2 vel = direction * throwForce;
        float timeStep = 0.05f;
        Vector2 gravity = Physics2D.gravity * 1.5f;

        for (int i = 0; i < points; i++)
        {
            aimLine.SetPosition(i, pos);
            vel += gravity * timeStep;
            pos += vel * timeStep;
        }
    }

    private void HideAimLine()
    {
        if (aimLine != null)
            aimLine.enabled = false;
    }
}