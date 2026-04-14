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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _charging = true;
            _currentCharge = 0f;
        }

        if (_charging && Input.GetMouseButton(0))
        {
            _currentCharge = Mathf.Min(_currentCharge + chargeSpeed * Time.deltaTime, maxForce);
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
        if (projectilePrefab == null) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        GameObject arrow = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb?.AddForce(direction * _currentCharge, ForceMode2D.Impulse);

        TurnManager.Instance?.EndTurn();
    }

    private void DrawAimLine()
    {
        if (aimLine == null) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        // simuluj trajektorii šípu
        aimLine.enabled = true;
        int points = 20;
        aimLine.positionCount = points;

        Vector2 pos = transform.position;
        Vector2 vel = direction * _currentCharge;
        float timeStep = 0.05f;

        for (int i = 0; i < points; i++)
        {
            aimLine.SetPosition(i, pos);
            vel += Physics2D.gravity * timeStep;
            pos += vel * timeStep;
        }
    }

    private void HideAimLine()
    {
        if (aimLine != null)
            aimLine.enabled = false;
    }

    protected override void Fire() { } // pøepsáno výše pøes Update
}