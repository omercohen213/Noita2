using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRate = 0.2f;

    private float _nextFireTime;

    private void Update()
    {
        // Check for left mouse click and rate limit
        if (Input.GetMouseButton(0) && Time.time >= _nextFireTime)
        {
            // Shoot to the direction based on the mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePos- transform.position).normalized;
            _nextFireTime = Time.time + _fireRate;
            Shoot(direction);
        }
    }

    private void Shoot(Vector3 direction)
    {
        // Create projectile
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);   
        projectile.GetComponent<Projectile>().Launch(direction);
    }
}
