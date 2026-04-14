using UnityEngine;
using System.Collections;

public class LightningWeapon : WeaponBase
{
    [Header("Blesk")]
    public int damage = 50;
    public float range = 20f;
    public LineRenderer lightningLine;
    public float flashDuration = 0.2f;

    protected override void Fire()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 direction = (mouseWorld - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range);

        if (hit.collider != null)
        {
            StartCoroutine(ShowLightning(transform.position, hit.point));

            if (hit.collider.TryGetComponent<MouseController>(out var mouse))
                mouse.TakeDamage(damage);
        }
        else
        {
            // nezas·hl nic ñ nakresli Ë·ru do max vzd·lenosti
            Vector3 endPoint = transform.position + (Vector3)(direction * range);
            StartCoroutine(ShowLightning(transform.position, endPoint));
        }

        TurnManager.Instance?.EndTurn();
    }

    private IEnumerator ShowLightning(Vector3 start, Vector3 end)
    {
        if (lightningLine == null) yield break;

        lightningLine.enabled = true;
        lightningLine.SetPosition(0, start);
        lightningLine.SetPosition(1, end);

        yield return new WaitForSeconds(flashDuration);

        lightningLine.enabled = false;
    }
}