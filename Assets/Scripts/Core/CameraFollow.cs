using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Sledování")]
    public float smoothSpeed = 3f;
    public Vector2 offset = new Vector2(0f, 2f);
    public float minY = -5f;
    public float maxY = 10f;

    private Transform _target;

    private void Update()
    {
        if (TurnManager.Instance?.ActiveMouse != null)
            _target = TurnManager.Instance.ActiveMouse.transform;

        if (_target == null) return;

        Vector3 targetPos = new Vector3(
            _target.position.x + offset.x,
            Mathf.Clamp(_target.position.y + offset.y, minY, maxY),
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position, targetPos, smoothSpeed * Time.deltaTime);
    }
}