using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float _lifetime;
    [SerializeField] private int _damage;
    [SerializeField] private int _force;

    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    public void Launch(Vector2 direction)
    {
        // Apply force to the rigidbody
        _rigidBody.AddForce(direction * _force, ForceMode2D.Impulse);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // You could call a TakeDamage() method here
            Debug.Log($"Hit {collision.collider.name} for {_damage} damage!");
            Destroy(gameObject);
        }
    }
}
