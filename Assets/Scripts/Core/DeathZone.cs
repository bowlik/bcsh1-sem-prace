using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<MouseController>(out var mouse))
        {
            Debug.Log($"{col.gameObject.name} spadla do propasti!");
            mouse.TakeDamage(9999);
        }

        if (col.TryGetComponent<Projectile>(out _))
            Destroy(col.gameObject);
    }
}