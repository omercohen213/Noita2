using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveAcceleration = 50f;
    [SerializeField] private float _maxMoveSpeed = 10f;
    [SerializeField] private float _gravityScale = 5f;
    [SerializeField] private float _floatSpeed = 10f; // Upward force while holding up

    private Rigidbody2D _rb;
    private Vector2 _moveInput;
    private bool _isGrounded;
    [SerializeField] private Transform _groundCheck; // An object on the player to identify ground
    [SerializeField] private float _groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f; // We'll handle gravity manually
    }

    void Update()
    {
        // Horizontal and vertical input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical"); // Up = float
        _moveInput = new Vector2(moveX, moveY).normalized;

        // Ground check
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        // Horizontal movement
        float targetVelX = _moveInput.x * _maxMoveSpeed;
        float accelRate = _isGrounded ? _moveAcceleration : _moveAcceleration;
        float newVelX = Mathf.MoveTowards(_rb.velocity.x, targetVelX, accelRate * Time.fixedDeltaTime);

        // Vertical movement (floating)
        float newVelY = _rb.velocity.y;
        if (_moveInput.y > 0) // Holding up
        {
            newVelY = _floatSpeed;
        }

        _rb.velocity = new Vector2(newVelX, newVelY);
    }

    private void HandleRotation()
    {
        Vector3 scale = transform.localScale;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePosition.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x); // Face right
        }
        else if (mousePosition.x < transform.position.x)
        {
            scale.x = -Mathf.Abs(scale.x); // Face left
        }
        transform.localScale = scale;
    }

    private void ApplyGravity()
    {
        // If player is not holding up, apply gravity
        if (_moveInput.y <= 0)
        {
            float newVelY = _rb.velocity.y - (_gravityScale * 9.81f * Time.fixedDeltaTime);
            _rb.velocity = new Vector2(_rb.velocity.x, newVelY);
        }
    }
}
