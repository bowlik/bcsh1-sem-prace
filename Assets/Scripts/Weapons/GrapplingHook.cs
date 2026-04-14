using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("Lano")]
    public float hookSpeed = 20f;
    public float pullSpeed = 8f;
    public float maxDistance = 15f;
    public LineRenderer ropeRenderer;

    private MouseController _owner;
    private Rigidbody2D _ownerRb;
    private Vector2 _hookPoint;
    private bool _isAttached = false;
    private bool _isFlying = false;
    private Vector2 _hookVelocity;
    private Vector2 _currentHookPos;

    public void Initialize(MouseController owner)
    {
        _owner = owner;
        _ownerRb = owner.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_owner.IsActive) return;

        if (Input.GetKeyDown(KeyCode.E))
            ShootHook();

        if (Input.GetKeyUp(KeyCode.E))
            ReleaseHook();

        if (_isFlying)
            UpdateFlyingHook();

        if (_isAttached)
            PullOwner();

        UpdateRopeVisual();
    }

    private void ShootHook()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        _currentHookPos = _owner.transform.position;
        _hookVelocity = ((Vector2)mouseWorld - _currentHookPos).normalized * hookSpeed;
        _isFlying = true;
        _isAttached = false;

        if (ropeRenderer != null)
            ropeRenderer.enabled = true;
    }

    private void UpdateFlyingHook()
    {
        _currentHookPos += _hookVelocity * Time.deltaTime;

        // zkontroluj kolizi s terénem nebo pøekážkou
        RaycastHit2D hit = Physics2D.Raycast(
            _currentHookPos - _hookVelocity * Time.deltaTime * 0.5f,
            _hookVelocity.normalized,
            _hookVelocity.magnitude * Time.deltaTime
        );

        float dist = Vector2.Distance(_owner.transform.position, _currentHookPos);

        if (hit.collider != null)
        {
            // lano se zachytilo
            _hookPoint = hit.point;
            _isFlying = false;
            _isAttached = true;
        }
        else if (dist > maxDistance)
        {
            // pøíliš daleko – zrušíme
            ReleaseHook();
        }
    }

    private void PullOwner()
    {
        Vector2 direction = (_hookPoint - (Vector2)_owner.transform.position).normalized;
        _ownerRb.linearVelocity = direction * pullSpeed;

        // odpoj když jsme blízko
        if (Vector2.Distance(_owner.transform.position, _hookPoint) < 0.5f)
            ReleaseHook();
    }

    private void ReleaseHook()
    {
        _isAttached = false;
        _isFlying = false;

        if (ropeRenderer != null)
            ropeRenderer.enabled = false;
    }

    private void UpdateRopeVisual()
    {
        if (ropeRenderer == null || (!_isFlying && !_isAttached)) return;

        ropeRenderer.SetPosition(0, _owner.transform.position);
        ropeRenderer.SetPosition(1, _isAttached ? (Vector3)_hookPoint : (Vector3)_currentHookPos);
    }
}