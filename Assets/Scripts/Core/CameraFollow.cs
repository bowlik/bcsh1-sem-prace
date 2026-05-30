using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Sledování")]
    public float smoothSpeed = 3f;
    public Vector2 offset = new Vector2(0f, 2f);
    public float minY = -5f;
    public float maxY = 10f;

    private Transform _target;
    private Transform _bombTarget;

    public void TrackBomb(Transform bomb)
    {
        _bombTarget = bomb;
    }

    public void StopTrackingBomb()
    {
        _bombTarget = null;
    }

    private void Update()
    {
        // priorita: granát > aktivní myš
        if (_bombTarget != null)
            _target = _bombTarget;
        else if (TurnManager.Instance?.ActiveMouse != null)
            _target = TurnManager.Instance.ActiveMouse.transform;

        if (_target == null) return;

        Vector3 targetPos = new Vector3(
            _target.position.x + offset.x,
            Mathf.Clamp(_target.position.y + offset.y, minY, maxY),
            transform.position.z);

        transform.position = Vector3.Lerp(
            transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}