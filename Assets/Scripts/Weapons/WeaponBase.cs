using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("Zbraò")]
    public string weaponName = "Zbraò";
    public float shootForce = 10f;
    public GameObject projectilePrefab;

    protected MouseController Owner;
    protected bool _hasFired = false;

    public void Initialize(MouseController owner)
    {
        Owner = owner;
    }

    public void ResetFired()
    {
        _hasFired = false;
    }

    private void OnEnable()
    {
        _hasFired = false;
    }

    private void Update()
    {
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
        Vector2 direction = (mouseWorld - Owner.transform.position).normalized;
        Vector3 spawnPos = Owner.transform.position + (Vector3)(direction * 0.8f);

        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        proj.GetComponent<Projectile>()?.SetShooter(Owner.gameObject);

        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.AddForce(direction * shootForce, ForceMode2D.Impulse);
        else
            Debug.LogError($"{proj.name} nemá Rigidbody2D!");

        TurnManager.Instance?.EndTurn();
    }
}