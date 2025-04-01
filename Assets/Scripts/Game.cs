using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private CubeFactory _cubeFactory;
    [SerializeField] private LayerMask _cubeLayer;
    [SerializeField] private Vector3[] _spawnPoints;

    private const int LeftMouseButton = 0;

    private GameSettings _settings = new();

    private void Start()
    {
        foreach (Vector3 position in _spawnPoints)
        {
            Cube cube = _cubeFactory.GetFromPool();
            cube.transform.position = position;
            cube.Initialize(_settings.StartingCubeScale, _settings.StartingCubeChildChance);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(LeftMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _cubeLayer))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cube cube))
                {
                    OnCubeClicked(cube);
                }
            }
        }
    }

    private void OnCubeClicked(Cube cube)
    {
        if (Random.Range(0, _settings.StartingCubeChildChance) < cube.ChildMakeChance)
        {
            Vector3 newScale = cube.Scale / 2;
            float newChance = cube.ChildMakeChance / 2;
            int newCubesCount = Random.Range(_settings.MinCubes, _settings.MaxCubes);

            for (int i = 0; i < newCubesCount; i++)
            {
                Cube newCube = _cubeFactory.GetFromPool();
                newCube.Initialize(newScale, newChance);
            }
            
            _cubeFactory.ReturnToPool(cube);
        }
    }

    private void MakeExplosion(Vector3 position, float force)
    {
        
    }
}