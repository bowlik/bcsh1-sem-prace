using UnityEngine;
using System.Collections;

public class PickaxeWeapon : WeaponBase
{
    [Header("Krumpáè")]
    public float destroyRadius = 0.8f;    // velikost výkopu
    public int swingCount = 3;            // poèet úderù za tah
    public float swingCooldown = 0.4f;    // pauza mezi údery
    public float reach = 1.5f;            // dosah od myši

    [Header("Efekty")]
    public GameObject dirtEffectPrefab;   // prefab èástic hlíny

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

        // Zkontroluj, jestli hráè kliknul v dosahu
        float dist = Vector2.Distance(Owner.transform.position, mouseWorld);
        if (dist > reach)
        {
            Debug.Log("Pøíliš daleko! Klikni blíže k postavì.");
            return;
        }

        StartCoroutine(Swing(mouseWorld));
    }

    private IEnumerator Swing(Vector3 targetPos)
    {
        _onCooldown = true;

        // 1. Vizuální efekt (èástice)
        if (dirtEffectPrefab != null)
        {
            Instantiate(dirtEffectPrefab, targetPos, Quaternion.identity);
        }

        // 2. Zvukový efekt pøes AudioManager
        AudioManager.Instance?.PlayPickaxe();

        // 3. Samotná destrukce terénu
        TerrainManager.Instance?.DestroyTerrain(targetPos, destroyRadius);

        _swingsLeft--;
        Debug.Log($"Krumpáè: zbývá {_swingsLeft} úderù");

        yield return new WaitForSeconds(swingCooldown);
        _onCooldown = false;

        // Po vyèerpání úderù ukonèi tah
        if (_swingsLeft <= 0)
        {
            _swingsLeft = swingCount; // Reset pro další kolo
            TurnManager.Instance?.EndTurn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Zobraz dosah v editoru (dosah kopání a velikost díry)
        if (Owner == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Owner.transform.position, reach);

        // Poznámka: destroyRadius je zobrazen u myši, zde jen ilustraènì u hráèe
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, destroyRadius);
    }
}