using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class MouseController : MonoBehaviour
{
    [Header("Pohyb")]
    public float moveSpeed = 4f;
    public float jumpForce = 8f;

    [Header("HP")]
    public int maxHp = 100;
    public int currentHp;

    [Header("Tým")]
    public int team = 1;

    [Header("UI")]
    public GameObject hpBarPrefab;

    private Rigidbody2D _rb;
    private bool _isActive = false;
    private bool _isGrounded = false;
    public bool IsActive => _isActive;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentHp = maxHp;
    }

    private void Start()
    {
        GameManager.Instance.RegisterMouse(this, team);

        // Vytvoø HP bar nad myší
        if (hpBarPrefab != null)
        {
            GameObject bar = Instantiate(hpBarPrefab, transform);
            // Pozice 1.2f nad støedem objektu
            bar.transform.localPosition = new Vector3(0, 1.2f, 0);

            // Inicializace HP baru, pokud komponenta existuje
            bar.GetComponent<HPBar>()?.Initialize(this);
        }
    }

    private void Update()
    {
        if (!_isActive) return;

        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _rb.linearVelocity = new Vector2(horizontal * moveSpeed, _rb.linearVelocity.y);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Zkontroluj, jestli stojíme na nìèem (detekce kolize zespodu)
        foreach (ContactPoint2D contact in col.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                _isGrounded = true;
                break;
            }
        }
    }

    public void SetActive(bool active)
    {
        _isActive = active;

        if (activeArrow != null)
            activeArrow.SetActive(active);
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(0, currentHp);
        Debug.Log($"{gameObject.name} má {currentHp} HP");

        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        GameManager.Instance.OnMouseDied(this);
        Destroy(gameObject);
    }
}