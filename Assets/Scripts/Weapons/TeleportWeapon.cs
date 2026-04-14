using UnityEngine;

public class TeleportWeapon : WeaponBase
{
    [Header("Teleportace")]
    public GameObject teleportEffectPrefab; // particle efekt (volitelné)

    protected override void Fire()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // zkontroluj jestli cílové místo není uvnitø terénu
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld);
        if (hit != null && hit.gameObject.CompareTag("Terrain"))
        {
            Debug.Log("Nelze teleportovat do terénu!");
            return;
        }

        // efekt na pùvodním místì
        if (teleportEffectPrefab != null)
            Instantiate(teleportEffectPrefab, Owner.transform.position, Quaternion.identity);

        // pøesuò myš
        Owner.transform.position = mouseWorld;

        // efekt na novém místì
        if (teleportEffectPrefab != null)
            Instantiate(teleportEffectPrefab, mouseWorld, Quaternion.identity);

        TurnManager.Instance?.EndTurn();
    }
}