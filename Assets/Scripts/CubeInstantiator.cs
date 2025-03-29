using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeInstantiator : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _explosionForce = 5.0f;
    [SerializeField] [Range(1f, 3f)] private float _densityFactor = 1.5f;

    private int _minCubeCount = 2;
    private int _maxCubeCount = 7;
    private float _oneThird = 1f / 3f;
    private float _scaleDivider = 2f;
    private float _explosionRadiusMultiplier = 2f;

    private void Start()
    {
        Vector3 defaultPosition = Vector3.up;
        Vector3 defaultScale = Vector3.one;
        float defaultRadius = 1;
        InstantiateNewCube(defaultPosition, defaultScale, defaultRadius);
    }

    private void OnCubeClicked(float splitChance, Vector3 position, Vector3 scale, Cube cube)
    {
        cube.CubeClicked -= OnCubeClicked;
        
        if (Random.value > splitChance) 
            return;

        float volume = scale.x * scale.y * scale.z;
        float radius = Mathf.Pow(volume, _oneThird) * _densityFactor;
        int cubesToSpawn = Random.Range(_minCubeCount, _maxCubeCount);
        
        for (int i = 0; i < cubesToSpawn; i++)
        {
            InstantiateNewCube(position, scale, radius);
        }
    }

    private void InstantiateNewCube(Vector3 position, Vector3 scale, float radius)
    {
        Cube newCube = Instantiate(
            _cubePrefab,
            position + Random.insideUnitSphere * radius,
            Quaternion.identity
        );

        newCube.transform.localScale = scale / _scaleDivider;
        newCube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            
        newCube.GetComponent<Rigidbody>()
            .AddExplosionForce(_explosionForce, position, radius * _explosionRadiusMultiplier, 0f, ForceMode.Impulse);

        newCube.CubeClicked += OnCubeClicked;
    }
}