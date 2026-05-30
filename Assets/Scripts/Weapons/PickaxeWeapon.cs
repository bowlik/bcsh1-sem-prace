using UnityEngine;
using System.Collections;

public class PickaxeWeapon : WeaponBase
{
    [Header("Krumpáè")]
    public int tilesToDestroy = 3;
    public float destroyRadius = 0.8f;
    public float swingCooldown = 0.4f;

    [Header("Efekty")]
    public GameObject dirtEffectPrefab;

    private bool _onCooldown = false;

    private void OnEnable()
    {
        _onCooldown = false;
    }

    protected override void Fire()
    {
        if (_onCooldown) return;
        StartCoroutine(Swing());
    }

    private IEnumerator Swing()
    {
        _onCooldown = true;
        _hasFired = true;

        AudioManager.Instance?.PlayPickaxe();

        Vector2 pos = Owner.transform.position;

        // znièí tiles vlevo od myši
        for (int i = 1; i <= tilesToDestroy; i++)
        {
            Vector2 leftPos = new Vector2(pos.x - i * 0.5f, pos.y);
            TerrainManager.Instance?.DestroyTerrain(leftPos, destroyRadius);

            if (dirtEffectPrefab != null)
                Instantiate(dirtEffectPrefab, leftPos, Quaternion.identity);
        }

        // znièí tiles vpravo od myši
        for (int i = 1; i <= tilesToDestroy; i++)
        {
            Vector2 rightPos = new Vector2(pos.x + i * 0.5f, pos.y);
            TerrainManager.Instance?.DestroyTerrain(rightPos, destroyRadius);

            if (dirtEffectPrefab != null)
                Instantiate(dirtEffectPrefab, rightPos, Quaternion.identity);
        }

        yield return new WaitForSeconds(swingCooldown);

        _onCooldown = false;
        TurnManager.Instance?.EndTurn();
    }

    private void OnDrawGizmosSelected()
    {
        if (Owner == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(Owner.transform.position, destroyRadius * tilesToDestroy);
    }
}