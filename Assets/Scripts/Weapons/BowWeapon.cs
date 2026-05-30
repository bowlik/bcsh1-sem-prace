using UnityEngine;

public class BowWeapon : WeaponBase
{
    [Header("Luk")]
    public float minForce = 5f;
    public float maxForce = 20f;
    public float chargeSpeed = 10f;
    public LineRenderer aimLine;

    private float _currentCharge = 0f;
    private bool _charging = false;

    private void OnEnable()
    {
        _charging = false;
        _currentCharge = 0f;
        HideAimLine();
    }

    private void Update()
    {
        if (Owner == null || !Owner.IsActive) return;
        if (_hasFired) return;

        if (Input.GetMouseButtonDown(0))
        {
            _charging = true;
            _currentCharge = minForce;
        }

        if (_charging && Input.GetMouseButton(0))
        {
            _currentCharge = Mathf.Min(
                _currentCharge + chargeSpeed * Time.deltaTime, maxForce);
            DrawAimLine();
        }

        if (_charging && Input.GetMouseButtonUp(0))
        {
            _charging = false;
            HideAimLine();
            FireArrow();
        }
    }

    private void FireArrow()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("BowWeapon: Projectile Prefab není pøiøazený!");
            return;
        }

        _hasFired = true;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - Owner.transform.position).normalized;
        Vector3 spawnPos = Owner.transform.position + (Vector3)(direction * 1.0f);

        GameObject arrow = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        arrow.GetComponent<Projectile>()?.SetShooter(Owner.gameObject);

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb?.AddForce(direction * _currentCharge, ForceMode2D.Impulse);

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
        Vector2 vel = direction * _currentCharge;
        float timeStep = 0.05f;

        // gravity scale 2 – stejné jako Arrow prefab
        Vector2 gravity = Physics2D.gravity * 2f;

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

    protected override void Fire() { }
}