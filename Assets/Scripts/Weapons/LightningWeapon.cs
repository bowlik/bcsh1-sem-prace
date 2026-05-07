using UnityEngine;
using System.Collections;

public class LightningWeapon : WeaponBase
{
    [Header("Blesk")]
    public int damage = 50;
    public float range = 20f;
    public LineRenderer lightningLine;
    public float flashDuration = 0.2f;
    public GameObject sparkEffectPrefab;
    public float knockbackForce = 15f;

    protected override void Fire()
    {
        _hasFired = true;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 startPos = Owner.transform.position;
        Vector2 direction = ((Vector2)mouseWorld - startPos).normalized;

        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;

        RaycastHit2D[] results = new RaycastHit2D[10];
        int hitCount = Physics2D.Raycast(startPos, direction, filter, results, range);

        Vector2 endPoint = startPos + direction * range;

        for (int i = 0; i < hitCount; i++)
        {
            RaycastHit2D hit = results[i];

            if (hit.collider.gameObject == Owner.gameObject) continue;
            if (hit.collider.transform.IsChildOf(Owner.transform)) continue;

            endPoint = hit.point;

            // poškození myši + knockback
            if (hit.collider.TryGetComponent<MouseController>(out var mouse))
            {
                mouse.TakeDamage(damage);

                // knockback ve smìru blesku
                Rigidbody2D rb = mouse.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }

            // zniš terén
            TerrainManager.Instance?.DestroyTerrain(hit.point, 1.5f);

            // spark efekt
            if (sparkEffectPrefab != null)
                Instantiate(sparkEffectPrefab, hit.point, Quaternion.identity);

            break;
        }

        StartCoroutine(ShowLightning(startPos, endPoint));
        TurnManager.Instance?.EndTurn();
    }

    private IEnumerator ShowLightning(Vector3 start, Vector3 end)
    {
        if (lightningLine == null)
        {
            Debug.LogWarning("LightningWeapon: LineRenderer není pøiøazený!");
            yield break;
        }

        lightningLine.enabled = true;
        lightningLine.SetPosition(0, start);
        lightningLine.SetPosition(1, end);

        yield return new WaitForSeconds(flashDuration);

        lightningLine.enabled = false;
    }
}