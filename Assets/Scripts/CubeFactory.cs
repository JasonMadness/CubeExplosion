using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeFactory : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    
    private List<Cube> _pool = new List<Cube>();

    public Cube GetFromPool()
    {
        Cube cube = _pool.FirstOrDefault(cube => cube.gameObject.activeSelf == false);
        
        if (cube == null)
            cube = Create();
        
        return cube;
    }

    public void ReturnToPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    private Cube Create()
    {
        Cube newCube = Instantiate(_cubePrefab, transform, true);
        newCube.gameObject.SetActive(false);
        _pool.Add(newCube);
        return newCube;
    }
}