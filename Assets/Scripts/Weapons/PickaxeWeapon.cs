using UnityEngine;
using System.Collections;

public class PickaxeWeapon : WeaponBase
{
    [Header("Krumpáè")]
    public float destroyRadius = 0.8f;    // velikost výkopu
    public int swingCount = 3;            // poèet úderù za tah
    public float swingCooldown = 0.4f;    // pauza mezi údery
    public float reach = 1.5f;            // dosah od myši

    private int _swingsLeft;
    private bool _onCooldown = false;

    private void OnEnable()
    {
        _swingsLeft = swingCount;
    }

    protected override void Fire()
    {
        if (_onCooldown || _swingsLeft <= 0) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // zkontroluj jestli kliknul v dosahu myši
        float dist = Vector2.Distance(Owner.transform.position, mouseWorld);
        if (dist > reach)
        {
            Debug.Log("Pøíliš daleko! Klikni blíže k myši.");
            return;
        }

        StartCoroutine(Swing(mouseWorld));
    }

    private IEnumerator Swing(Vector3 targetPos)
    {
        _onCooldown = true;

        // vykopni díru do terénu
        TerrainManager.Instance?.DestroyTerrain(targetPos, destroyRadius);

        // TODO: pøidat animaci švihu / zvuk

        _swingsLeft--;
        Debug.Log($"Krumpáè: zbývá {_swingsLeft} úderù");

        yield return new WaitForSeconds(swingCooldown);
        _onCooldown = false;

        // po vyèerpání úderù ukonèi tah
        if (_swingsLeft <= 0)
        {
            _swingsLeft = swingCount;
            TurnManager.Instance?.EndTurn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // zobraz dosah v editoru
        if (Owner == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Owner.transform.position, reach);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Owner.transform.position, destroyRadius);
    }
}