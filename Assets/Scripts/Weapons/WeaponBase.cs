using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Zbraò")]
    public string weaponName = "Zbraò";
    public float shootForce = 10f;
    public GameObject projectilePrefab;

    protected MouseController Owner;
    private bool _hasFired = false;

    public void Initialize(MouseController owner)
    {
        Owner = owner;
    }

    private void OnEnable()
    {
        // reset pøi zapnutí zbranì
        _hasFired = false;
    }

    private void Update()
    {
        // støílej pouze pokud je myš aktivní a zbraò ještì nevystøelila
        if (Owner == null || !Owner.IsActive) return;
        if (_hasFired) return;

        if (Input.GetMouseButtonDown(0))
            Fire();
    }

    protected virtual void Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning($"{weaponName}: Projectile Prefab není pøiøazený!");
            return;
        }

        _hasFired = true;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        Vector2 direction = (mouseWorld - transform.position).normalized;

        GameObject proj = Instantiate(
            projectilePrefab, transform.position, Quaternion.identity);

        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb?.AddForce(direction * shootForce, ForceMode2D.Impulse);

        TurnManager.Instance?.EndTurn();
    }
}