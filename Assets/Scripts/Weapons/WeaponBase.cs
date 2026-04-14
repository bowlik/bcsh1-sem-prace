using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Zbraò")]
    public string weaponName = "Zbraò";
    public float shootForce = 10f;
    public GameObject projectilePrefab;

    protected MouseController Owner;

    public void Initialize(MouseController owner)
    {
        Owner = owner;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Fire();
    }

    protected virtual void Fire()
    {
        if (projectilePrefab == null) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 direction = (mouseWorld - transform.position).normalized;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb?.AddForce(direction * shootForce, ForceMode2D.Impulse);

        // po výstøelu konec tahu
        TurnManager.Instance?.EndTurn();
    }
}