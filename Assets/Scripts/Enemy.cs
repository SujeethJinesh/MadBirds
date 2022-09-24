using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _cloudParticlePrefab;
    [SerializeField] private float killVelocity = 15;
    [SerializeField] private float obstacleKillVelocity = 7;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bird bird = collision.collider.GetComponent<Bird>();
        Obstacle obstacle = collision.collider.GetComponent<Obstacle>();
        if (collision.relativeVelocity.magnitude > killVelocity ||
            bird != null ||
            (obstacle != null && collision.relativeVelocity.magnitude > obstacleKillVelocity) ||
            collision.contacts[0].normal.y < -0.5)
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }
    }
}
