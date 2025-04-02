using UnityEngine;

public class Exploder : MonoBehaviour
{
    [Header("Explosion Force Settings")]
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _upwardModifier = 0.5f;
    
    [Header("Random Direction Settings")]
    [SerializeField] private float _minHorizontalDirection = -1f;
    [SerializeField] private float _maxHorizontalDirection = 1f;
    [SerializeField] private float _minVerticalDirection = 0.5f;
    [SerializeField] private float _maxVerticalDirection = 1f;
    
    [Header("Distance Settings")]
    [SerializeField] private float _minExplosionDistance = 1f;
    [SerializeField] private float _maxExplosionDistance = 3f;

    public void Explode(Vector3 position, Cube[] cubes)
    {
        foreach (Cube cube in cubes)
        {
            Vector3 randomDirection = new Vector3(
                Random.Range(_minHorizontalDirection, _maxHorizontalDirection),
                Random.Range(_minVerticalDirection, _maxVerticalDirection),
                Random.Range(_minHorizontalDirection, _maxHorizontalDirection)
            );
            
            float distance = Random.Range(_minExplosionDistance, _maxExplosionDistance);
            
            cube.transform.position = position + randomDirection * distance;
            
            Rigidbody rigidbody = cube.GetComponent<Rigidbody>();
            rigidbody.AddExplosionForce(_explosionForce, position, _explosionRadius, _upwardModifier, ForceMode.Impulse);
        }
    }
} 